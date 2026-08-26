using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Projects
{
    public interface IGetProjectsQuery : IQuery<object?, IEnumerable<ProjectResponse>>
    {
    }
}
