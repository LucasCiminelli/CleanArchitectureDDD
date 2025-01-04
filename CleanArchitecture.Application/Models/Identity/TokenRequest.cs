using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Models.Identity
{
    public class TokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

    }
}
