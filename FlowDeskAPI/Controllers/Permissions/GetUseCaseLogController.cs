using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Permissions;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Permissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUseCaseLogController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetUseCaseLogQuery query,
                                  [FromServices] PermissionHandler handler,
                                  [FromQuery] PagedRequest request)
        {
            var result = handler.ExecuteQuery(query, request);
            return Ok(result);
        }
    }
}
