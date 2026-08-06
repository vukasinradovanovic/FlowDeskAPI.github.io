using Application;
using System.Collections.Generic;

namespace FlowDesk.API.JWT
{
    public class JwtUser : IApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> AllowedUseCases { get; set; } = new List<string> {  };

        
    }
}
