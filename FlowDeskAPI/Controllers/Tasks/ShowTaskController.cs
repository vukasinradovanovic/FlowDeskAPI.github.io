using Application.Flowdesk.Queries.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowTaskController : ControllerBase
    {
        [HttpGet("{slug}")]
        public ActionResult Index([FromServices] IGetTaskBySlugQuery query,
                                  [FromServices] PermissionHandler handler,
                                  string slug)
        {
            var task = handler.ExecuteQuery(query, slug);
            return Ok(task);
        }
    }
}
