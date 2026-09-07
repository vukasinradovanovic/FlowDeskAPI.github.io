using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Implementation.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Queries.Tasks
{
    public class EfGetAllTasksQuery : EfPermissions, IGetAllTasksQuery
    {
        public EfGetAllTasksQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.ViewTasksId;

        public string Name => _defaultPermissionSettings.ViewTasksName;

        public PagedResponse<TaskResponse> Execute(PagedRequest request)
        {
            var tasks = _context.Tasks.AsNoTracking();
            return tasks.Paginate(request, t => new TaskResponse
            {
                Id = t.Id,
                Slug = t.Slug,
                Name = t.Name,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                UpdatedAt = t.UpdatedAt,
                AssignedUserId = t.AssignedUser.Id,
                ProjectId = t.ProjectId,
                Status = new StatusResponse
                {
                    Id = t.Status.Id,
                    Name = t.Status.Name,
                    Theme = t.Status.StatusTheme
                }
            });
        }
    }
}
