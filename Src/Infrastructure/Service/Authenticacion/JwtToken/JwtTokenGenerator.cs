using Aplication.Interfaces.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Service.Authenticacion.JwtToken
{
    public class JwtTokenGenerator(string secretKey, int expiresIn): ITokenGenerator
    {
        private readonly string _secretKey = secretKey;
        private readonly int _expiresIn = expiresIn;
        public string GenerateToken(string fullName)
        {
            List<Claim> claims =
            [
                new Claim("FullName", fullName)
            ];

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwt = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(_expiresIn),
                signingCredentials: credentials,
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
