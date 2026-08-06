namespace FlowDeskAPI.DTO.Autentification
{
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarColor { get; set; }
        public string Role { get; set; }
        public IEnumerable<PermissionResponse> Permissions { get; set; }

    }
}
