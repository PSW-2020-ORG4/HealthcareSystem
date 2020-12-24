using Microsoft.IdentityModel.Tokens;
using PatientWebApp.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PatientWebApp
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _tokenKey;
        public JWTAuthenticationManager(string tokenKey)
        {
            _tokenKey = tokenKey;
        }
        public string Authenticate(string username, string jmbg, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username), new Claim("Jmbg",jmbg) , new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public LoggedUserDTO GetLoggedUser(string token)
        {
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            string username = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string role = claims.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            string jmbg = claims.Claims.First(c => c.Type == "Jmbg").Value;

            return new LoggedUserDTO(username, jmbg, role);
        }
    }
}
