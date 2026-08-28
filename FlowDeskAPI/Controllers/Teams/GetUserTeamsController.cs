using Application.Flowdesk.Queries.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUserTeamsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromServices] IGetUsersTeamQuery query,
                                  [FromServices] PermissionHandler handler)
        {
            var teams = handler.ExecuteQuery(query, null);
            return Ok(teams);
        }
    }
}
