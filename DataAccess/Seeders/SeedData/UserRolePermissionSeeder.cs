using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class UserRolePermissionSeeder : IDataSeeder<UserRolePermission>
    {
        public IEnumerable<UserRolePermission> GetSeedData()
        {
            return new List<UserRolePermission>
            {
                new UserRolePermission { UserRoleId = 1, PermissionId = 1 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 2 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 3 },
                new UserRolePermission { UserRoleId = 2, PermissionId = 1 },
                new UserRolePermission { UserRoleId = 2, PermissionId = 2 },
                new UserRolePermission { UserRoleId = 3, PermissionId = 1 }
            };
        }
    }
}
