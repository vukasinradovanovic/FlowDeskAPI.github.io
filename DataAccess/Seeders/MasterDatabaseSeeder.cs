using DataAccess.FlowDesk.Seeders.SeedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders
{
    // Master database seeder that executes all individual seeders in a specific order
    public class MasterDatabaseSeeder
    {
        public static void Execute(FlowDbContext context)
        {
            context.Database.EnsureCreated();

            var seederSequence = new List<ISeeder>
            {
                new StatusSeeder(),
                new RoleSeeder(),
                new TeamSeeder(),
                new PermissionSeeder(),
                new UserSeeder(),
                new ProjectSeeder(),
                new UserRoleSeeder(),
                new UserRolePermissionSeeder(),
                new AuthTokenSeeder(),
            };

            foreach (var seeder in seederSequence)
            {
                seeder.Seed(context);
            }
        }
    }
}
