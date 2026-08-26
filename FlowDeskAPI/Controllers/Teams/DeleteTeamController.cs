using Application.Flowdesk.Commands.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteTeamController : ControllerBase
    {
        [HttpDelete("{id}")]
        public ActionResult Delete([FromServices] IDeleteTeamCommand command,
                                   [FromServices] PermissionHandler hendler,
                                   [FromRoute] int id)
        {
            hendler.ExecuteCommand(command, id);
            return StatusCode(204);
        }
    }
}
