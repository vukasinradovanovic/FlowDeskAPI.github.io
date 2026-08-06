using Domain.ProjectTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class TeamSeeder : IDataSeeder<Team>
    {
        public IEnumerable<Team> GetSeedData()
        {
            return new List<Team>
            {
                new Team{ Name = "Development Team" },
                new Team{ Name = "Design Team" },
                new Team{ Name = "Marketing Team" },
            };
        }
    }
}
