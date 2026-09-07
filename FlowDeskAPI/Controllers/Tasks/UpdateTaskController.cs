using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.DTO.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateTaskController : ControllerBase
    {
        [HttpPut("{slug}")]
        [Consumes("multipart/form-data")]
        public ActionResult Index([FromServices] IUpdateTaskCommand command,
                                  [FromServices] PermissionHandler handler,
                                  [FromForm] UpdateTaskRequest request,
                                  [FromRoute] string slug)
        {
            request.Slug = slug;
            handler.ExecuteCommand(command, request);
            return Ok();
        }
    }
}
