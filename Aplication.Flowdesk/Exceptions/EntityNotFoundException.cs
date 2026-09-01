namespace Application.Flowdesk.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType) : base("Entity of type " + entityType + " was not found.")
        {

        }
    }
}
