using Application;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Projects
{
    public class EfGetAllUserProjects : EfPermissions, IGetProjectsQuery
    {
        private readonly IApplicationUser _user;
        public EfGetAllUserProjects(FlowDbContext context,
                                    IOptions<DefaultPermissionSettings> defaultPermissionSettings,
                                    IApplicationUser user)
                    : base(context, defaultPermissionSettings.Value)
        {
            _user = user;
        }

        public int Id => _defaultPermissionSettings.ViewUserProjectsId;
        public string Name => _defaultPermissionSettings.ViewUserProjectsName;

        public IEnumerable<ProjectResponse> Execute(object? request)
        {
            return _context.Projects
                .Where(p => p.ProjectTeams.Any(pt => pt.Team.Members.Any(m => m.UserId == _user.Id)))
                .Select(p => new ProjectResponse
                {
                    Name = p.Name,
                    Slug = p.Slug,
                    Icon = p.Icon,
                    Theme = p.Theme,
                    DueDate = p.DueDate,
                    CreatedAt = p.CreatedAt
                })
                .ToList();
        }
    }
}
