using Application;
using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using FlowDeskAPI.DTO.Autentification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Teams
{
    public class EfGetUserTeamsQuery : EfPermissions, IGetUsersTeamQuery
    {
        private readonly IApplicationUser _currentUser;
        public EfGetUserTeamsQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IApplicationUser currentUser) : base(context, defaultPermissionSettings.Value)
        {
            _currentUser = currentUser;
        }

        public int Id => _defaultPermissionSettings.ViewUserProjectsId;

        public string Name => _defaultPermissionSettings.ViewUserProjectsName;

        public PagedResponse<TeamResponse> Execute(PagedRequest? request)
        {
            var teams = _context.Teams.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request?.Keyword))
            {
                var keyword = request.Keyword.Trim();
                teams = teams.Where(t => t.Name.Contains(keyword));
            }

            return teams
                .AsNoTracking()
                .Where(t => t.Members.Any(m => m.UserId == _currentUser.Id))
                .OrderBy(t => t.Id)
                .Paginate(request, t => new TeamResponse
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

                    Projects = t.ProjectTeams
                    .Where(tp => tp.Project != null)
                    .Select(tp => new ProjectResponse
                    {
                        Id = tp.Project.Id,
                        Name = tp.Project.Name,
                        Slug = tp.Project.Slug,
                        Icon = tp.Project.Icon,
                        Theme = tp.Project.Theme,
                        DueDate = tp.Project.DueDate,
                        CreatedAt = tp.Project.CreatedAt,
                        Status = tp.Project.Status == null ? null : new StatusResponse
                        {
                            Id = tp.Project.Status.Id,
                            Name = tp.Project.Status.Name,
                            Theme = tp.Project.Status.StatusTheme
                        },
                        Teams = tp.Project.ProjectTeams
                            .Where(pt => pt.Team != null)
                            .Select(pt => new TeamResponse
                            {
                                Id = pt.Team.Id,
                                Name = pt.Team.Name
                            })
                    })
                });
        }
    }
}
