using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Teams
{
    public interface IGetUsersTeamQuery : IQuery<object?, IEnumerable<TeamResponse>>
    {
    }
}
