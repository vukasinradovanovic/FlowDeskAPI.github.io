using Application;

namespace Implementation
{
    public class UnauthorizedUser : IApplicationUser
    {
        public int Id => 0;
        public string Username => "guest";
        public string FirstName => "Guest";
        public string LastName => "Guest";
        public string Email => "guest@gmail.com";
        public IEnumerable<String> Permissions => new List<string> { "Guest Permissions", "View User Projects" };
    }
}