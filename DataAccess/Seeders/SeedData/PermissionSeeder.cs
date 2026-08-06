using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class PermissionSeeder : IDataSeeder<Permission>
    {
        public IEnumerable<Permission> GetSeedData()
        {
            return new List<Permission>
            {
                new Permission { Name = "View Projects" },
                new Permission { Name = "Create Projects" },
                new Permission { Name = "Edit Projects" },
                new Permission { Name = "Delete Projects" },
                new Permission { Name = "View Tasks" },
                new Permission { Name = "Create Tasks" },
                new Permission { Name = "Edit Tasks" },
                new Permission { Name = "Delete Tasks" }
            };
        }
    }
}
