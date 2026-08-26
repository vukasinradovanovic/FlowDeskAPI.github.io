using Application.Flowdesk.Queries.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllUsersProjectsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAllUsersProjects(
            [FromServices] IGetProjectsQuery query,
            [FromQuery] PermissionHandler hendler)
        {
            var projects = hendler.ExecuteQuery(query, null);
            return Ok(projects);
        }
    }
}
