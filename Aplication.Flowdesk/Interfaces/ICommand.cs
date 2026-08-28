namespace Application.Flowdesk.Interfaces
{
    public interface ICommand<TRequest> : IPermission
    {
        void Execute(TRequest request);
    }
}
