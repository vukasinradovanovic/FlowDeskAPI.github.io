using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Projects
{
    public interface IUpdateProjectCommand : ICommand<UpdateProjectRequest>
    {
    }
}
