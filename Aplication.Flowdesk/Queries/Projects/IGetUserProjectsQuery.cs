using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Projects
{
    public interface IGetUserProjectsQuery : IQuery<PagedRequest, PagedResponse<ProjectResponse>>
    {
    }
}
