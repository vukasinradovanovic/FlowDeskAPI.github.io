using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Domain.Identity
{
    public class UserRolePermission : BaseEntity
    {
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
