using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly CleanArchitectureIdentityDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthService
            (
                UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager,
                IOptions<JwtSettings> jwtSettings,
                CleanArchitectureIdentityDbContext context,
                TokenValidationParameters tokenValidationParameters
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }

            var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!resultado.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await GenerateToken(user);
            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName,
            };

            return authResponse;
        }

        public async Task<AuthResponse> RefereshToken(TokenRequest request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParamsClone = _tokenValidationParameters.Clone();
            tokenValidationParamsClone.ValidateLifetime = false;

            try
            {
                //validation: El formato del token es correcto
                var tokenVerification = jwtTokenHandler.ValidateToken(
                    request.Token,
                    tokenValidationParamsClone,
                    out var validatedToken
                );

                //validation: Verifica la encriptación
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return new AuthResponse
                        {
                            Success = false,
                            Errors = new List<string>
                            {
                                "El token tiene errores de encriptación"
                            }
                        };
                    }
                }

                //validation: Verificar fecha de expiración
                var utcExpiryDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token ha expirado"
                        }
                    };
                }

                //validación: el refresh token exista en la base de datos
                var storedToken = await _context.RefreshTokens!.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
                if (storedToken is null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token no existe"
                        }
                    };
                }

                //validación: El token ya fue usado
                if (storedToken.IsUsed)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token ya fue usado"
                        }
                    };
                }

                //validación: El token fue revocado.
                if (storedToken.IsRevoked)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token fue revocado."
                        }
                    };
                }

                //validación: El id del token
                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token no concuerda con el valor inicial"
                        }
                    };
                }

                //validación: fecha de expiración del storedtoken
                if (storedToken.ExpireDate < DateTime.UtcNow)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El refresh token ha expirado"
                        }
                    };
                }

                storedToken.IsUsed = true;
                _context.RefreshTokens!.Update(storedToken);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(storedToken.UserId);
                var token = await GenerateToken(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Token = token.Item1,
                    Email = user.Email,
                    Username = user.UserName,
                    RefreshToken = token.Item2,
                    Success = true
                };


            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Lifetime validation failed. The token is expired"))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token ha expirado por favor tiene que volver a realizar el login"
                        }
                    };
                }
                else
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "El token tiene errores, tienes que volver a hacer el login"
                        }
                    };
                }
            }



        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }


        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                throw new Exception($"El username ya fue tomado por otra cuenta");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new Exception($"El email ya fue tomado por otra cuenta");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                var token = await GenerateToken(user);
                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id,
                    Username = user.UserName,
                };
            }

            throw new Exception($"{result.Errors}");

        }


        private async Task<Tuple<string, string>> GenerateToken(IdentityUser user)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 }.Union(userClaims).Union(roleClaims)),

                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMonths(6),
                Token = $"{GenerateRandomTokenCharacters(35)}{Guid.NewGuid()}"
            };

            await _context.RefreshTokens!.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new Tuple<string, string>(jwtToken, refreshToken.Token);

        }


        private string GenerateRandomTokenCharacters(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());

        }


    }
}
