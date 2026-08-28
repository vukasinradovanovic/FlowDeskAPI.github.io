using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Projects
{
    public interface ICreateProjectCommand : ICommand<CreateProjectRequest>
    {
    }
}
