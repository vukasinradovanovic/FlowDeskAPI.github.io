namespace Application.Flowdesk.Interfaces
{
    /// <summary>
    /// Contract for CQRS Query operations (read requests that return data without modifying state).
    /// Implements IPermission to enforce security checks at the request level.
    /// </summary>
    /// <typeparam name="TParam">
    /// The input parameters/filter criteria required to execute the query (e.g., GetProjectByIdRequest).
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The return data structure sent back to the caller (e.g., ProjectResponseDto). Must be a class.
    /// </typeparam>
    public interface IQuery<TParam, TResponse> : IPermission
        where TResponse : class
    {
        /// <summary>
        /// Executes the query logic using the provided request parameters.
        /// </summary>
        /// <param name="request">The input payload containing query filters or criteria.</param>
        /// <returns>An instance of TResponse containing the requested data, or null if not found.</returns>
        TResponse Execute(TParam request);
    }
}
