using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Flowdesk.DTO.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string AvatarColor { get; set; }
    }
}
