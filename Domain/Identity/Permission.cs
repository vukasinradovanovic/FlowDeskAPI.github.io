using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new HashSet<UserRolePermission>();
    }
}
