using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Queries.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllTeamsController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index([FromServices] IGetAllTeamsQuery query,
                                  [FromServices] PermissionHandler handler,
                                  [FromQuery] PagedRequest request)
        {
            var teams = handler.ExecuteQuery(query, request);
            return Ok(teams);
        }
    }
}
