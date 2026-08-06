using Domain.ProjectTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarColor { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public virtual ICollection<ProjectTask> AssignedTasks { get; set; } = new HashSet<ProjectTask>();
        public virtual ICollection<UserTeam> UserTeams { get; set; } = new HashSet<UserTeam>();
    }
}
