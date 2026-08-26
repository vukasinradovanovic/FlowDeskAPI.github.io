using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.DTO.Auth;
using Implementation.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(
            [FromServices] IRegisterUserCommand cmd,
            [FromServices] PermissionHandler handler,
            [FromBody] RegisterRequest request
            )
        {
            handler.ExecuteCommand(cmd, request);
            return StatusCode(201);
        }
    }
}
