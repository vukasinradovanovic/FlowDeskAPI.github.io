using Domain.Identity;

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
                    Username = "sarah1",
                    FirstName = "Sarah",
                    LastName = "Jenkins",
                    Email = "sarah@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "emerald"
                },
                new User
                {
                    Username = "john1",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "indigo"
                },
                new User
                {
                    Username = "emily1",
                    FirstName = "Emily",
                    LastName = "Smith",
                    Email = "emily@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "amber"
                },
                new User
                {
                    Username = "michael1",
                    FirstName = "Michael",
                    LastName = "Brown",
                    Email = "michael@flowdesk.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    AvatarColor = "rose"
                },
                new User
                {
                    Username = "jessica1",
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