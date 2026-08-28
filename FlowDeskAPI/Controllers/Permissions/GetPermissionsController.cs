using Application.Flowdesk.Queries.Permissions;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Permissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPermissionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetPermissionsQuery query,
                                  [FromServices] PermissionHandler handler)
        {
            var data = handler.ExecuteQuery(query, null);
            return Ok(data);
        }
    }
}
