namespace CleanArchitecture.Application.Models.Identity
{
    public class AuthResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Token { get; set; } = string.Empty ;
        public string RefreshToken { get; set; } = string.Empty;
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
    }
}
