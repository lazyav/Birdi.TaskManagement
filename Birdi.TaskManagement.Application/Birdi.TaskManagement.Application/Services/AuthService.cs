using Birdi.TaskManagement.Application.Contract;
using Birdi.TaskManagement.Core.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                var claims = new[] { new Claim("id", userId.ToString()) };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.KEY));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                   _jwtSettings.Issuer,
                   _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn);

                return new JwtSecurityTokenHandler().WriteToken(token);
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
