using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrbanControl.Backend.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace UrbanControl.Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config= config;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        public string GenerarTokenj(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],   // <--- ¿Es igual a "UrbanControl.Backend"?
    audience: _config["Jwt:Audience"], // <--- ¿Es igual a "UrbanControl.Angular"?
    claims: claims,
    expires: DateTime.Now.AddDays(1),
    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
