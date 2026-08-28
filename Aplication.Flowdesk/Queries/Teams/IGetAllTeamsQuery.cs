using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Teams
{
    public interface IGetAllTeamsQuery : IQuery<object?, IEnumerable<TeamResponse>>
    {
    }
}
