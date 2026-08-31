using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.DTO.CreateTeamRequest;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Teams
{
    public class EfCreateTeamCommand : EfPermissions, ICreateTeamCommand
    {
        public EfCreateTeamCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.CreateTeamsId;

        public string Name => _defaultPermissionSettings.CreateTeamsName;

        public void Execute(CreateTeamRequest request)
        {
            var team = new Team
            {
                Name = request.Name,
            };
            _context.Teams.Add(team);
            _context.SaveChanges();
        }
    }
}
