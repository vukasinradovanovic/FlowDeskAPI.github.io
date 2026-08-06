using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class UserSeeder : IDataSeeder<User>
    {
        public IEnumerable<User> GetSeedData()
        {
            return new List<User>
            {
                new User
                {
                    FirstName = "Sarah",
                    LastName = "Jenkins",
                    Email = "sarah@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "emerald"
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "indigo"
                },
                new User
                {
                    FirstName = "Emily",
                    LastName = "Smith",
                    Email = "emily@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "amber"
                },
                new User
                {
                    FirstName = "Michael",
                    LastName = "Brown",
                    Email = "michael@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "rose"
                },
                new User
                {
                    FirstName = "Jessica",
                    LastName = "Davis",
                    Email = "jessica@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "emerald"
                }
            };
        }
    }
}