namespace Birdi.TaskManagement.Core.Entity
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
    }
}
