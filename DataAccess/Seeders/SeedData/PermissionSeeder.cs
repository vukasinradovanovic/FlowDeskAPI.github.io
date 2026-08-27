using Domain.Identity;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class PermissionSeeder : IDataSeeder<Permission>
    {
        public IEnumerable<Permission> GetSeedData()
        {
            return new List<Permission>
            {
                new Permission { Name = "Guest Permissions" },
                new Permission { Name = "View User Projects" },
                new Permission { Name = "View Projects" },
                new Permission { Name = "Create Projects" },
                new Permission { Name = "Edit Projects" },
                new Permission { Name = "Delete Projects" },
                new Permission { Name = "View Tasks" },
                new Permission { Name = "Create Tasks" },
                new Permission { Name = "Edit Tasks" },
                new Permission { Name = "Delete Tasks" },
                new Permission { Name = "Create Teams" },
                new Permission { Name = "Edit Teams" },
                new Permission { Name = "Delete Teams" },
                new Permission { Name = "View Teams" },

            };
        }
    }
}
