using Application.Flowdesk.DTO.CreateTeamRequest;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Teams
{
    public interface ICreateTeamCommand : ICommand<CreateTeamRequest>
    {
    }
}
