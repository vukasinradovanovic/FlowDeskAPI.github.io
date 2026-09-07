using Application.Flowdesk.DTO.Attachments;
using Application.Flowdesk.DTO.Statuses;

namespace Application.Flowdesk.DTO.Tasks
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
        public StatusResponse Status { get; set; }
        public IEnumerable<AttachmentResponse> Attachments { get; set; } = new List<AttachmentResponse>();

    }
}
