using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IApplicationUser
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public IEnumerable<string> AllowedUseCases { get; }
    }
}
