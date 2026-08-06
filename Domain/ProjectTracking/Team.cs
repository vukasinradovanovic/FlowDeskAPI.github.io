using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProjectTracking
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<ProjectTeam> ProjectTeams { get; set; } = new HashSet<ProjectTeam>();
        public virtual ICollection<UserTeam> Members { get; set; } = new HashSet<UserTeam>();

    }
}
