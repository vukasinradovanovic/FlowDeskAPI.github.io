using Application.Flowdesk.DTO.Pagination;
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
            [FromServices] IGetUserProjectsQuery query,
            [FromServices] PermissionHandler hendler,
            [FromQuery] PagedRequest request)
        {
            var projects = hendler.ExecuteQuery(query, request);
            return Ok(projects);
        }
    }
}
