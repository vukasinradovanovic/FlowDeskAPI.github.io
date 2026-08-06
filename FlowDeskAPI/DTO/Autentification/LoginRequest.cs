using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Flowdesk.DTO.Autentification
{
    public class LoginRequest 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
