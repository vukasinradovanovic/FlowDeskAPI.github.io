using Microsoft.AspNetCore.Http;

namespace Application.Flowdesk.DTO.Tasks
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
        public IEnumerable<IFormFile>? Attachments { get; set; }
    }
}
