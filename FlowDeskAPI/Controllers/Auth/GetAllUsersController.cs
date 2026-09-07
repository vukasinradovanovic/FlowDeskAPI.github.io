using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Auth;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllUsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetAllUsersQuery query,
                                  [FromServices] PermissionHandler handler,
                                  [FromQuery] PagedRequest? request)
        {
            var users = handler.ExecuteQuery(query, request);
            return Ok(users);
        }
    }
}
