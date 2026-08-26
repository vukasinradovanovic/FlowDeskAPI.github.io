using Domain.ProjectTracking;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class ProjectTeamSeeder : IDataSeeder<ProjectTeam>
    {
        public IEnumerable<ProjectTeam> GetSeedData()
        {
            return new List<ProjectTeam>
            {
                new ProjectTeam { ProjectId = 1, TeamId = 1 },
                new ProjectTeam { ProjectId = 2, TeamId = 2 },
            };
        }
    }
}
