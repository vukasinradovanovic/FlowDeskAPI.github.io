namespace Application.Flowdesk.Interfaces
{
    /// <summary>
    /// Contract for CQRS Command operations (state-changing actions like Create, Update, or Delete).
    /// Implements IPermission to enforce security checks before executing business logic.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The input command payload carrying data necessary to execute the action (e.g., CreateProjectRequest).
    /// </typeparam>
    public interface ICommand<TRequest> : IPermission
    {
        /// <summary>
        /// Executes the state-modifying business logic using the provided command request.
        /// </summary>
        /// <param name="request">The input payload containing values required for the operation.</param>
        void Execute(TRequest request);
    }
}
