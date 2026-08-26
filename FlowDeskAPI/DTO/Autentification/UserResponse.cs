using Application.Flowdesk.DTO.Auth;

namespace FlowDeskAPI.DTO.Autentification
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarColor { get; set; }
        public IEnumerable<PermissionResponse> Permissions { get; set; }
        public RoleResponse Role { get; set; }

    }
}
