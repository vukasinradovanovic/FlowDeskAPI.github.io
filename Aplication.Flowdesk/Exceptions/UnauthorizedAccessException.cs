using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnauthorizedUseCaseException : Exception
    {
        public UnauthorizedUseCaseException(int id, string firstName, string lastName, string email, string useCaseName)
            : base($"User with ID:{id} and credentials: {firstName} {lastName}  has tried to execute {useCaseName}. Email of the user is {email}.")
        {
            
        }
    }
}
