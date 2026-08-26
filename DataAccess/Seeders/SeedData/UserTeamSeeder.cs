using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class UserTeamSeeder : IDataSeeder<UserTeam>
    {
        public IEnumerable<UserTeam> GetSeedData()
        {
            return new List<UserTeam>
            {
                new UserTeam { UserId = 1, TeamId = 1 },
                new UserTeam { UserId = 2, TeamId = 1 },
                new UserTeam { UserId = 3, TeamId = 2 },
                new UserTeam { UserId = 4, TeamId = 2 },
            };
        }
    {
    }
}
