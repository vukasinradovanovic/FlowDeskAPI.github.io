using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.DTO.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateProjectController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create([FromServices] ICreateProjectCommand command,
                                   [FromServices] PermissionHandler handler,
                                   [FromBody] CreateProjectRequest request)
        {
            handler.ExecuteCommand(command, request);
            return StatusCode(201);
        }
    }
}
