using Application.Flowdesk.Queries.Projects;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Projects
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowProjectController : ControllerBase
    {
        [HttpGet("{slug}")]
        public ActionResult Show([FromServices] IGetProjectBySlugQuery query,
                                 [FromServices] PermissionHandler handler,
                                 string slug)
        {
            var project = handler.ExecuteQuery(query, slug);
            return Ok(project);
        }
    }
}
