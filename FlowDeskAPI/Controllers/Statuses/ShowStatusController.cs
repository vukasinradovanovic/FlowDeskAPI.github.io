using Application.Flowdesk.Queries.Statuses;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Statuses
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowStatusController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Show([FromServices] IGetStatusByIdQuery query,
                                 [FromServices] PermissionHandler handler,
                                 int id)
        {
            var status = handler.ExecuteQuery(query, id);
            return Ok(status);
        }
    }
}
