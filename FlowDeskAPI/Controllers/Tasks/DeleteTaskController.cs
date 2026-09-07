using Application.Flowdesk.Commands.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteTaskController : ControllerBase
    {
        [HttpDelete("{slug}")]
        public ActionResult Delete([FromServices] IDeleteTaskCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromRoute] string slug)
        {
            handler.ExecuteCommand(command, slug);
            return NoContent();
        }
    }
}
