using Domain.ProjectTracking;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class TeamSeeder : IDataSeeder<Team>
    {
        public IEnumerable<Team> GetSeedData()
        {
            return new List<Team>
            {
                new Team { Name = "Development Team" },
                new Team { Name = "Design Team" },
                new Team { Name = "Marketing Team" },
                new Team { Name = "QA & Testing Team" },
                new Team { Name = "DevOps & Infrastructure" },
                new Team { Name = "Product Management" },
                new Team { Name = "Customer Success" },
                new Team { Name = "Security & Compliance" }
            };
        }
    }
}
