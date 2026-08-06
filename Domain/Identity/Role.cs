using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
