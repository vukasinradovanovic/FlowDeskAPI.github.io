namespace Application
{
    public interface IApplicationUser
    {
        public int Id { get; }
        public string Username { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public IEnumerable<string> Permissions { get; }
    }
}
