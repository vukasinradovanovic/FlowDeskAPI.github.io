using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.DTO.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateProjectController : ControllerBase
    {
        [HttpPut("{slug}")]
        public ActionResult Update([FromServices] IUpdateProjectCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromBody] UpdateProjectRequest request,
                                   [FromRoute] string slug)
        {
            request.Slug = slug;
            handler.ExecuteCommand(command, request);
            return Ok();
        }
    }
}
