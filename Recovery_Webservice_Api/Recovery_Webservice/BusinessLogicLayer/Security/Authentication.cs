using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace Recovery_Webservice.BusinessLogicLayer.Security
{
    public class Authentication
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public Authentication()
        {
            _secretKey = ConfigurationManager.AppSettings["AuthJwtSecretKey"];
            _issuer = ConfigurationManager.AppSettings["AuthJwtIssuer"];
            _audience = ConfigurationManager.AppSettings["AuthJwtAudience"];

            if (string.IsNullOrEmpty(_secretKey) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
            {
                throw new InvalidOperationException("JWT configuration values are not set correctly in web.config.");
            }
        }

        public void Validate()
        {
            var context = HttpContext.Current;
            var request = context.Request;
            var authHeader = request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                HandleUnauthorizedRequest(context, "Authorization header is missing or malformed.");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var principal = ValidateToken(token);
                HttpContext.Current.User = principal;
            }
            catch (SecurityTokenException ex)
            {
                HandleUnauthorizedRequest(context, $"Token validation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                HandleUnauthorizedRequest(context, $"An error occurred: {ex.Message}");
            }
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero 
            };
            return tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
        }

        private void HandleUnauthorizedRequest(HttpContext context, string message)
        {
            context.Response.StatusCode = 401;
            context.Response.StatusDescription = "Unauthorized";
            context.Response.Write($"Unauthorized request: {message}");
            context.Response.End();
        }
    }
}