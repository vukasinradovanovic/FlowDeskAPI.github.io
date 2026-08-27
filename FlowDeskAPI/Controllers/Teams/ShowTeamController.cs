using Application.Flowdesk.Queries.Teams;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowTeamController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Show([FromServices] IGetTeamByIdQuery query,
                                 [FromServices] PermissionHandler handler,
                                  int id)
        {
            var team = handler.ExecuteQuery(query, id);
            return Ok(team);
        }
    }
}
