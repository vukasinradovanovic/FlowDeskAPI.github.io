using Application;
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
    public class EfGetUserTasks : EfPermissions, IGetUsersTasksQuery
    {
        private readonly IApplicationUser _currentUser;
        public EfGetUserTasks(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IApplicationUser currentUser) : base(context, defaultPermissionSettings.Value)
        {
            _currentUser = currentUser;
        }

        public int Id => _defaultPermissionSettings.ViewUserTasksId;

        public string Name => _defaultPermissionSettings.ViewUserTasksName;

        public PagedResponse<TaskResponse> Execute(PagedRequest? request)
        {
            var tasks = _context.Tasks
                            .AsNoTracking()
                            .Where(t => t.AssignedUserId == _currentUser.Id);

            if (!string.IsNullOrWhiteSpace(request?.Keyword))
            {
                var keyword = request.Keyword.Trim();
                tasks = tasks.Where(t => t.Name.Contains(keyword));
            }

            return tasks
                .OrderByDescending(t => t.CreatedAt)
                .Paginate(request, t => new TaskResponse
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
