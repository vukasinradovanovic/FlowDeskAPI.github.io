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
        public ActionResult Create([FromServices] ICreateTaskCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromBody] CreateTaskRequest request)
        {
            handler.ExecuteCommand(command, request);
            return Created();
        }
    }
}
