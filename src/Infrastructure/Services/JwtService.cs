using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("El email del usuario no puede ser nulo o vacío.", nameof(user.Email));

            var jwtSettings = _configuration.GetSection("JwtSettings");

            string? secretKeyString = jwtSettings["Secret"];
            if (string.IsNullOrWhiteSpace(secretKeyString))
                throw new InvalidOperationException("La clave secreta (Secret) no está configurada en JwtSettings.");

            if (!double.TryParse(jwtSettings["ExpirationMinutes"], out double expirationMinutes))
                throw new InvalidOperationException("El valor de ExpirationMinutes en JwtSettings no es válido.");

            var secretKey = Encoding.UTF8.GetBytes(secretKeyString);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id ?? throw new ArgumentException("El Id del usuario no puede ser nulo.", nameof(user.Id))),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("FullName", user.FullName ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}