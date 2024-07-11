using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Core.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Birdi.TaskManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateToken(Guid userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userId", userId.ToString()),
                    }),
                    Expires = DateTime.Now.AddMinutes(_jwtSettings.TokenValidityInMinutes),
                    SigningCredentials = new SigningCredentials(_jwtSettings.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to generate token.");
            }
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new Exception("Invalid Token");

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = _jwtSettings.ValidateIssuer,
                    ValidateAudience = _jwtSettings.ValidateAudience,
                    ValidateLifetime = _jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = _jwtSettings.IssuerSigningKey,
                    ClockSkew = TimeSpan.Zero
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
                return principal;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to generate refresh token.");
            }
        }
    }
}
