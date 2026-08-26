using DataAccess.FlowDesk.Seeders.SeedData;

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
                new ProjectTeamSeeder(),
                new UserTeamSeeder(),
            };

            foreach (var seeder in seederSequence)
            {
                seeder.Seed(context);
            }
        }
    }
}
