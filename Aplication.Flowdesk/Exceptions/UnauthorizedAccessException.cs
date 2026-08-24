namespace Application.Exceptions
{
    public class UnauthorizedPermissionException : Exception
    {
        public UnauthorizedPermissionException(int id, string username, string firstName, string lastName, string email, string permissionName)
            : base($"User with ID:{id}, Username: {username}, credentials: {firstName} {lastName}  has tried to execute {permissionName}. Email of the user is {email}.")
        {

        }
    }
}
