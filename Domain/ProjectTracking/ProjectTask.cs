using Domain.Identity;
using Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProjectTracking
{
    public class ProjectTask : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

    }
}
