using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index([FromServices] IGetProjectsQuery query,
                                   [FromServices] PermissionHandler handler,
                                   [FromQuery] PagedRequest request)
        {
            var projects = handler.ExecuteQuery(query, request);
            return Ok(projects);
        }
    }
}
