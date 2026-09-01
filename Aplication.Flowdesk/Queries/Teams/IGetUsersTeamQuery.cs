using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Teams
{
    public interface IGetUsersTeamQuery : IQuery<PagedRequest?, PagedResponse<TeamResponse>>
    {
    }
}
