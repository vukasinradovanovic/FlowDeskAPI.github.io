using Application;
using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
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
            var query = _context.Teams.Where(t => t.Members.Any(m => m.UserId == _currentUser.Id)).AsQueryable();

            return query.Paginate(request, t => new TeamResponse
            {
                Id = t.Id,
                Name = t.Name,
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
                        Id = tp.Project.Status.Id,
                        Name = tp.Project.Status.Name,
                        Theme = tp.Project.Status.StatusTheme
                    }

                })
            });
        }
    }
}
