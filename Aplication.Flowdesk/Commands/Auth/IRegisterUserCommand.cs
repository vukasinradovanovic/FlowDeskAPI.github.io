using Application.Flowdesk.DTO.Auth;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Commands.Auth
{
    public interface IRegisterUserCommand : ICommand<RegisterRequest>
    {
    }
}
