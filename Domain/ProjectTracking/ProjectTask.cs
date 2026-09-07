using Domain.Identity;
using Domain.Statuses;

namespace Domain.ProjectTracking
{
    public class ProjectTask : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<ProjectAttachment> Attachments { get; set; } = new List<ProjectAttachment>();

    }
}
