using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.DTO.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateTaskController : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public ActionResult Create([FromServices] ICreateTaskCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromForm] CreateTaskRequest request)
        {
            handler.ExecuteCommand(command, request);
            return Created();
        }
    }
}
