using Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class StatusSeeder : IDataSeeder<Status>
    {
        public IEnumerable<Status> GetSeedData()
        {
            return new List<Status>
            {
                new Status { Name = "To Do", StatusTheme = "badge-secondary" },
                new Status { Name = "At Risk", StatusTheme = "amber" },
                new Status { Name = "In Progress", StatusTheme = "badge-primary" },
                new Status { Name = "On Track", StatusTheme = "indigo" },
                new Status { Name = "Completed", StatusTheme = "badge-success" }
            };
        }
    }
}
