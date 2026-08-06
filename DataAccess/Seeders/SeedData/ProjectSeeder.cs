using Domain.ProjectTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class ProjectSeeder : IDataSeeder<Project>
    {
        public IEnumerable<Project> GetSeedData()
        {
            return new List<Project>
            {
                new Project
                {
                    Name = "Marketing Website Redesign",
                    Slug = "marketing-website-redesign",
                    Icon = "bi-palette",
                    Theme = "primary",
                    DueDate = DateTime.Now.AddDays(10),
                    CreatedAt = DateTime.Now,
                    StatusId = 3
                },
                new Project
                {
                    Name = "Mobile App V3.2",
                    Slug = "mobile-app-v3-2",
                    Icon = "bi-phone",
                    Theme = "emerald",
                    DueDate = DateTime.Now.AddDays(10),
                    CreatedAt = DateTime.Now,
                    StatusId = 4
                },
            };
        }
    }
}
