using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Interfaces;
namespace Application.Flowdesk.Queries.Tasks

{
    public interface IGetAllTasksQuery : IQuery<PagedRequest, PagedResponse<TaskResponse>>
    {
    }
}
