using Application.Flowdesk.DTO.Teams;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Teams
{
    public interface IGetUsersTeamQuery : IQuery<object?, IEnumerable<TeamResponse>>
    {
    }
}
