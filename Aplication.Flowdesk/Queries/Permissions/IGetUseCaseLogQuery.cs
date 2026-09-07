using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Permissions;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Permissions
{
    public interface IGetUseCaseLogQuery : IQuery<PagedRequest?, PagedResponse<UseCaseLogResponse>>
    {
    }
}
