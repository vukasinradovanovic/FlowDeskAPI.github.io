using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Teams
{
    public class EfDeleteTeamCommand : EfPermissions, IDeleteTeamCommand
    {
        public EfDeleteTeamCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.DeleteTeamsId;

        public string Name => _defaultPermissionSettings.DeleteTeamsName;

        public void Execute(int id)
        {
            var team = _context.Teams.Find(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
        }
    }
}
