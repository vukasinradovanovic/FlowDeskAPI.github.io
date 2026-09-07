using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Tasks
{
    public interface ICreateTaskCommand : ICommand<CreateTaskRequest>
    {
    }
}
