using Application.Flowdesk.Commands.Team;
using Application.Flowdesk.DTO.CreateTeamRequest;
using Implementation.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace FlowDeskAPI.Controllers.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateTeamController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create([FromServices] ICreateTeamCommand comand,
                                   [FromServices] PermissionHandler hendler,
                                   [FromBody] CreateTeamRequest request)
        {
            hendler.ExecuteCommand(comand, request);
            return StatusCode(201);
        }
    }
}
