using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Projects
{
    public class EfGetProjectBySlugQuery : EfPermissions, IGetProjectBySlugQuery
    {
        public EfGetProjectBySlugQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.EditProjectsId;

        public string Name => _defaultPermissionSettings.EditProjectsName;

        public ProjectResponse Execute(string request)
        {
            return _context.Projects
                .Where(p => p.Slug == request)
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Icon = p.Icon,
                    Theme = p.Theme,
                    DueDate = p.DueDate,
                    Status = p.Status == null ? null : new StatusResponse
                    {
                        Id = p.Status.Id,
                        Name = p.Status.Name,
                        Theme = p.Status.StatusTheme
                    }
                })
                .FirstOrDefault() ?? throw new ArgumentException("Project not found");
        }
    }
}
