using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Birdi.TaskManagement.Core.Config
{
    public class JwtSettings
    {
        public const string Section = "JWTKEY";
        public string KEY { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public SecurityKey IssuerSigningKey
        {
            get
            {
                byte[] encodedKey = Encoding.UTF8.GetBytes(KEY);
                return new SymmetricSecurityKey(encodedKey);
            }
        }
    }
}
