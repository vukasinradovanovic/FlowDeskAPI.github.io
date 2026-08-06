using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new HashSet<UserRolePermission>();

    }
}
