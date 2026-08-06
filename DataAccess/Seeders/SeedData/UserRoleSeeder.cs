using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class UserRoleSeeder : IDataSeeder<UserRole>
    {
        public IEnumerable<UserRole> GetSeedData()
        {
            return new List<UserRole>
            {
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 2 },
                new UserRole { UserId = 3, RoleId = 3 },
                new UserRole { UserId = 4, RoleId = 4 },
                new UserRole { UserId = 5, RoleId = 5 }
            };
        }
    }
}
