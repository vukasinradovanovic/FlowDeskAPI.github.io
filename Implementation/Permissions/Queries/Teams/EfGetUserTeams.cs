using Application;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.Teams;
using Application.Flowdesk.Queries.Teams;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Teams
{
    public class EfGetUserTeams : EfPermissions, IGetUsersTeamQuery
    {
        private readonly IApplicationUser _currentUser;
        public EfGetUserTeams(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IApplicationUser currentUser) : base(context, defaultPermissionSettings.Value)
        {
            _currentUser = currentUser;
        }

        public int Id => _defaultPermissionSettings.ViewUserProjectsId;

        public string Name => _defaultPermissionSettings.ViewUserProjectsName;

        public IEnumerable<TeamResponse> Execute(object? request)
        {
            return _context.Teams
                 .Where(t => t.Members.Any(m => m.UserId == _currentUser.Id))
                 .Select(t => new TeamResponse
                 {
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
                             Name = tp.Project.Status.Name,
                             Theme = tp.Project.Status.StatusTheme
                         }

                     }).ToList()
                 })
                 .ToList();
        }
    }
}
