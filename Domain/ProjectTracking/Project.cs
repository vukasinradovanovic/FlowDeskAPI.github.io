using Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProjectTracking
{
    public class Project : BaseEntity
    {
        public string Name {  get; set; }
        public string Slug { get; set; }
        public string Icon { get; set; }
        public string Theme { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<ProjectTeam> ProjectTeams { get; set; } = new HashSet<ProjectTeam>();
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new HashSet<ProjectTask>();

    }
}
