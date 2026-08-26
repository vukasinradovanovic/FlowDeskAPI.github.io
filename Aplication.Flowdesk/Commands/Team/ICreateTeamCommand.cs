using Application.Flowdesk.DTO.CreateTeamRequest;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Team
{
    public interface ICreateTeamCommand : ICommand<CreateTeamRequest>
    {
    }
}
