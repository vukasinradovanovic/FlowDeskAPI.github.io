using Application.Flowdesk.Queries.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllUserProjectsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAllUsersProjects(
            [FromServices] IGetProjectsQuery query,
            [FromServices] PermissionHandler hendler)
        {
            var projects = hendler.ExecuteQuery(query, null);
            return Ok(projects);
        }
    }
}
