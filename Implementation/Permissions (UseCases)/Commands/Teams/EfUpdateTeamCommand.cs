using Application.Flowdesk.Commands.Teams;
using Application.Flowdesk.DTO.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
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
            var team = _context.Teams
                            .Include(t => t.Members)
                            .FirstOrDefault(t => t.Id == request.Id);
            if (team == null)
            {
                throw new KeyNotFoundException($"Team with ID {request.Id} not found.");
            }

            team.Name = request.Name;

            var newMemberIds = request.UserIds ?? Enumerable.Empty<int>();

            var membersToRemove = team.Members
                .Where(m => !newMemberIds.Contains(m.UserId))
                .ToList();

            foreach (var member in membersToRemove)
            {
                team.Members.Remove(member);
            }

            var currentMemberIds = team.Members.Select(m => m.UserId).ToList();
            var membersToAdd = newMemberIds
                .Where(userId => !currentMemberIds.Contains(userId))
                .Select(userId => new UserTeam
                {
                    TeamId = team.Id,
                    UserId = userId
                })
                .ToList();

            foreach (var newMember in membersToAdd)
            {
                team.Members.Add(newMember);
            }

            _context.SaveChanges();
        }
    }
}
