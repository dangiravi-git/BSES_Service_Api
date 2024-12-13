using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Power_Metering.BusinessLogicLayer.Security
{
    public class JwtTokenGenerator
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        private readonly double _expirationTimeInHours;
        public JwtTokenGenerator()
        {
            _issuer = ConfigurationManager.AppSettings["AuthJwtIssuer"];
            _audience = ConfigurationManager.AppSettings["AuthJwtAudience"];
            _secretKey = ConfigurationManager.AppSettings["AuthJwtSecretKey"];
            _expirationTimeInHours = Convert.ToDouble(ConfigurationManager.AppSettings["AuthJwtExpirationTimeInHours"]);

            if (string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience) || string.IsNullOrEmpty(_secretKey))
            {
                throw new InvalidOperationException("JWT configuration is missing in web.config or appSettings.");
            }
        }
        public string GenerateToken(string username, string role)
        {
            try
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(_expirationTimeInHours),
                    signingCredentials: signingCredentials
                );
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(tokenDescriptor);
                return token;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while generating the token.", ex);
            }
        }
    }
}