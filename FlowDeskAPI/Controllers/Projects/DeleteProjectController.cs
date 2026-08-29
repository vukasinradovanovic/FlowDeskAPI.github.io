using Application.Flowdesk.Commands.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteProjectController : ControllerBase
    {
        [HttpDelete("{slug}")]
        public ActionResult Delete([FromServices] IDeleteProjectCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromRoute] string slug)
        {
            handler.ExecuteCommand(command, slug);
            return StatusCode(204);
        }
    }
}
