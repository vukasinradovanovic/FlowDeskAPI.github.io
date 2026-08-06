using Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public class UnauthorizedUser : IApplicationUser
    {
        public int Id => 0;
        public string FirstName => "Guest";
        public string LastName => "Guest";
        public string Email => "guest@gmail.com";
        public IEnumerable<string> AllowedUseCases => new List<string> { "register" };
    }
}