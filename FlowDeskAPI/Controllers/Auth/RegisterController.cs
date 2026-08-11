using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.DTO.Auth;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public ActionResult Register(
            [FromServices] IRegisterUserCommand cmd,
            [FromServices] UseCaseHandler handler,
            [FromBody] RegisterRequest request
            )
        {
            handler.ExecuteCommand(cmd, request);
            return StatusCode(201);
        }
    }
}
