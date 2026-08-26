using Application.Flowdesk.Queries.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUsersTeamController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetUsersTeam([FromServices] IGetUsersTeamQuery query,
                                         [FromServices] PermissionHandler hendler)
        {
            var teams = hendler.ExecuteQuery(query, null);
            return Ok(teams);
        }
    }
}
