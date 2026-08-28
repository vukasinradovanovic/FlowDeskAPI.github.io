using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Projects
{
    public class EfGetAllProjectsQuery : EfPermissions, IGetProjectsQuery
    {
        public EfGetAllProjectsQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.ViewProjectsId;

        public string Name => _defaultPermissionSettings.ViewProjectsName;

        public IEnumerable<ProjectResponse> Execute(object? request)
        {
            return _context.Projects.Select(p => new ProjectResponse
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
                    Theme = p.Status.StatusTheme,
                }
            }).ToList();
        }
    }
}
