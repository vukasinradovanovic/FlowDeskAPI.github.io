using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProjectTracking
{
    public class ProjectTeam : BaseEntity
    {
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
