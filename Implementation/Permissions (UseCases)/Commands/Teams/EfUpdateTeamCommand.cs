using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.DTO.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Teams
{
    public class EfUpdateTeamCommand : EfPermissions, IUpdateTeamCommand
    {
        public EfUpdateTeamCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.EditTeamsId;

        public string Name => _defaultPermissionSettings.EditTeamsName;

        public void Execute(UpdateTeamRequest request)
        {
            var team = _context.Teams.Find(request.Id);
            team.Name = request.Name;

            _context.Teams.Update(team);
            _context.SaveChanges();
        }
    }
}
