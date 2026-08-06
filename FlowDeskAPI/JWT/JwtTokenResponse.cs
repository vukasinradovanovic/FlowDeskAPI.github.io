using FlowDeskAPI.DTO.Autentification;

namespace FlowDesk.API.JWT
{
    public class JwtTokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
    }
}
