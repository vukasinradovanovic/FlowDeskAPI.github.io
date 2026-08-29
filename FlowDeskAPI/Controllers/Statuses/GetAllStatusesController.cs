using Application.Flowdesk.Queries.Statuses;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Statuses
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllStatusesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetAllStatusesQuery getAllStatusesQuery,
                                  [FromServices] PermissionHandler handler)
        {
            var statuses = handler.ExecuteQuery(getAllStatusesQuery, null);
            return Ok(statuses);
        }
    }
}
