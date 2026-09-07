using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Interfaces;
using FlowDeskAPI.DTO.Autentification;

namespace Application.Flowdesk.Queries.Auth
{
    public interface IGetAllUsersQuery : IQuery<PagedRequest?, PagedResponse<UserResponse>>
    {
    }
}
