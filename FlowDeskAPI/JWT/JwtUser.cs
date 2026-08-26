using Application;
using Application.Flowdesk.DTO.Auth;

namespace FlowDesk.API.JWT
{
    public class JwtUser : IApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Permissions { get; set; } = new List<string> { };
        public RoleResponse Role { get; set; }


    }
}
