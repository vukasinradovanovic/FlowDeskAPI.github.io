using Application;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Projects
{
    public class EfGetAllUserProjectsQuery : EfPermissions, IGetProjectsQuery
    {
        private readonly IApplicationUser _user;
        public EfGetAllUserProjectsQuery(FlowDbContext context,
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
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Icon = p.Icon,
                    Theme = p.Theme,
                    DueDate = p.DueDate,
                    CreatedAt = p.CreatedAt,
                    Status = new StatusResponse
                    {
                        Name = p.Status.Name,
                        Theme = p.Status.StatusTheme
                    }
                })
                .ToList();
        }
    }
}
