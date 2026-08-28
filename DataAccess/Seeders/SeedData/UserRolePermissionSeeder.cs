using Domain.Identity;

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
                new UserRolePermission { UserRoleId = 1, PermissionId = 4 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 5 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 6 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 7 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 8 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 9 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 10 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 11 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 12 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 13 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 14 },
                new UserRolePermission { UserRoleId = 1, PermissionId = 15 },
                new UserRolePermission { UserRoleId = 2, PermissionId = 1 },
                new UserRolePermission { UserRoleId = 2, PermissionId = 2 },
                new UserRolePermission { UserRoleId = 3, PermissionId = 1 }
            };
        }
    }
}
