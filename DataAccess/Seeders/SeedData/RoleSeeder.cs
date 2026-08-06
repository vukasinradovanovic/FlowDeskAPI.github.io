using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class RoleSeeder : IDataSeeder<Role>
    {
        public IEnumerable<Role> GetSeedData()
        {
            return new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "Project Manager" },
                new Role { Name = "Team Manager" },
                new Role { Name = "Team Lead" },
                new Role { Name = "User" },
            };
        }
    }
}
