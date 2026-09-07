using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Tasks;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllTasksController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetAllTasksQuery query,
                                  [FromServices] PermissionHandler handler,
                                  [FromQuery] PagedRequest request)
        {
            var tasks = handler.ExecuteQuery(query, request);
            return Ok(tasks);
        }
    }
}
