using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.DTO.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateTeamController : ControllerBase
    {
        [HttpPut]
        public ActionResult Update([FromServices] IUpdateTeamCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromBody] UpdateTeamRequest request)
        {
            handler.ExecuteCommand(command, request);
            return StatusCode(200);
        }
    }
}
