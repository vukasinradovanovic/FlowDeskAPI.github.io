using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.TeamDto;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.EntityFrameworkCore;
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

        public PagedResponse<ProjectResponse> Execute(PagedRequest? request)
        {
            return _context.Projects
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Paginate(request, p => new ProjectResponse
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
                    },
                    Teams = p.ProjectTeams
                    .Where(pt => pt.Team != null)
                    .Select(t => new TeamResponse
                    {
                        Id = t.Team.Id,
                        Name = t.Team.Name,
                    }).ToList()
                });
        }
    }
}
