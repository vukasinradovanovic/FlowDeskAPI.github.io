using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUserTasksController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetUsersTasksQuery query,
                                  [FromServices] PermissionHandler handler,
                                  [FromQuery] PagedRequest request)
        {
            var tasks = handler.ExecuteQuery(query, request);
            return Ok(tasks);
        }
    }
}
