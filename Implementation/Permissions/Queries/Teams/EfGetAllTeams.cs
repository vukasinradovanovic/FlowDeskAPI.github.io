using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using FlowDeskAPI.DTO.Autentification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Teams
{
    public class EfGetAllTeams : EfPermissions, IGetAllTeamsQuery
    {
        public EfGetAllTeams(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.ViewTeamsId;

        public string Name => _defaultPermissionSettings.ViewTeamsName;

        public IEnumerable<TeamResponse> Execute(object? request)
        {
            return _context.Teams
                .AsNoTracking()
                .Select(t => new TeamResponse
                {
                    Id = t.Id,
                    Name = t.Name,
                    Members = t.Members.Select(m => new UserResponse
                    {
                        Id = m.User.Id,
                        Email = m.User.Email,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName,
                        AvatarColor = m.User.AvatarColor
                    }).ToList(),
                    Projects = t.ProjectTeams.Select(tp => new ProjectResponse
                    {
                        Id = tp.Project.Id,
                        Name = tp.Project.Name,
                        Slug = tp.Project.Slug,
                        Icon = tp.Project.Icon,
                        Theme = tp.Project.Theme,
                        DueDate = tp.Project.DueDate,
                        CreatedAt = tp.Project.CreatedAt,
                        Status = new StatusResponse
                        {
                            Name = tp.Project.Status.Name,
                            Theme = tp.Project.Status.StatusTheme
                        }
                    }).ToList()
                })
                .ToList();
        }
    }
}
