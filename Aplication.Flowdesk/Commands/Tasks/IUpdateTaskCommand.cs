using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Tasks
{
    public interface IUpdateTaskCommand : ICommand<UpdateTaskRequest>
    {
    }
}
