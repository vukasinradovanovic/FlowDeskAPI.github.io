using Application;
using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Extentions;
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

        public PagedResponse<ProjectResponse> Execute(PagedRequest? request)
        {

            var query = _context.Projects.Where(p => p.ProjectTeams.Any(pt => pt.Team.Members.Any(m => m.UserId == _user.Id)));

            return query.Paginate(request, p => new ProjectResponse
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Icon = p.Icon,
                Theme = p.Theme,
                DueDate = p.DueDate,
                CreatedAt = p.CreatedAt,
                Status = p.Status == null ? null : new StatusResponse
                {
                    Id = p.Status.Id,
                    Name = p.Status.Name,
                    Theme = p.Status.StatusTheme,
                }
            });
        }
    }
}
