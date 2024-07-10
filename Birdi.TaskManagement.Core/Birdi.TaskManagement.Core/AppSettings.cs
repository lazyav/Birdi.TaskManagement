namespace Birdi.TaskManagement.Core
{
    public class AppSettings
    {
        public const string DbName = "Database";
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
    }
}
