using Application.Flowdesk.Commands.Auth;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivateAccountController : ControllerBase
    {
        [HttpGet("{activationCode}")]
        public ActionResult Activate([FromServices] IActivateAccountCommand command,
                                     [FromServices] PermissionHandler handler,
                                     string activationCode)
        {
            handler.ExecuteCommand(command, activationCode);
            return Redirect($"http://localhost:5173/activate?token={activationCode}");
        }
    }
}
