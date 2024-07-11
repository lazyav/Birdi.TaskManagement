namespace Birdi.TaskManagement.Application.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
    }
}
