using System;
using apiv2.Models;

namespace apiv2.Utilities.JWTUtils
{
    public interface IJWTUtils
    {
        public string GenerateJWTToken(Apprentice apprentice);
        public Guid ValidateJWTToken(string token);
    }
}
