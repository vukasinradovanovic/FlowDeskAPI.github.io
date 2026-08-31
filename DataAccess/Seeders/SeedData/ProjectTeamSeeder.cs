using Domain.ProjectTracking;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class ProjectTeamSeeder : IDataSeeder<ProjectTeam>
    {
        public IEnumerable<ProjectTeam> GetSeedData()
        {
            return new List<ProjectTeam>
            {
                // Projects assigned to Development Team (TeamId = 1)
                new ProjectTeam { ProjectId = 1, TeamId = 1 },
                new ProjectTeam { ProjectId = 3, TeamId = 1 },
                new ProjectTeam { ProjectId = 4, TeamId = 1 },
                new ProjectTeam { ProjectId = 7, TeamId = 1 },
                new ProjectTeam { ProjectId = 12, TeamId = 1 },
                new ProjectTeam { ProjectId = 14, TeamId = 1 },
                new ProjectTeam { ProjectId = 15, TeamId = 1 },
                new ProjectTeam { ProjectId = 17, TeamId = 1 },

                // Projects assigned to Design Team (TeamId = 2)
                new ProjectTeam { ProjectId = 1, TeamId = 2 },
                new ProjectTeam { ProjectId = 2, TeamId = 2 },
                new ProjectTeam { ProjectId = 6, TeamId = 2 },
                new ProjectTeam { ProjectId = 13, TeamId = 2 },
                new ProjectTeam { ProjectId = 19, TeamId = 2 },

                // Projects assigned to Marketing Team (TeamId = 3)
                new ProjectTeam { ProjectId = 1, TeamId = 3 },
                new ProjectTeam { ProjectId = 10, TeamId = 3 },
                new ProjectTeam { ProjectId = 16, TeamId = 3 },
                new ProjectTeam { ProjectId = 24, TeamId = 3 },

                // Projects assigned to QA & Testing Team (TeamId = 4)
                new ProjectTeam { ProjectId = 2, TeamId = 4 },
                new ProjectTeam { ProjectId = 11, TeamId = 4 },

                // Projects assigned to DevOps & Infrastructure (TeamId = 5)
                new ProjectTeam { ProjectId = 5, TeamId = 5 },
                new ProjectTeam { ProjectId = 21, TeamId = 5 },
                new ProjectTeam { ProjectId = 25, TeamId = 5 },

                // Projects assigned to Product Management (TeamId = 6)
                new ProjectTeam { ProjectId = 8, TeamId = 6 },
                new ProjectTeam { ProjectId = 18, TeamId = 6 },

                // Projects assigned to Customer Success (TeamId = 7)
                new ProjectTeam { ProjectId = 22, TeamId = 7 },

                // Projects assigned to Security & Compliance (TeamId = 8)
                new ProjectTeam { ProjectId = 9, TeamId = 8 },
                new ProjectTeam { ProjectId = 20, TeamId = 8 },
                new ProjectTeam { ProjectId = 23, TeamId = 8 }
            };
        }
    }
}
