namespace Birdi.TaskManagement.Core.Config
{
    public class JwtSettings
    {
        public const string Section = "JWT";
        public string KEY { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
