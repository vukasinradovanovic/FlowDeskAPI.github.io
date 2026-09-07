using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Tasks
{
    public class EfCreateTaskCommand : EfPermissions, ICreateTaskCommand
    {
        private readonly StatusSettings _status;

        public EfCreateTaskCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IOptions<StatusSettings> statusSettings) : base(context, defaultPermissionSettings.Value)
        {
            _status = statusSettings.Value;
        }

        public int Id => _defaultPermissionSettings.CreateTasksId;

        public string Name => _defaultPermissionSettings.CreateTasksName;

        public void Execute(CreateTaskRequest request)
        {
            string baseSlug = request.Name.Trim().ToLower().Replace(" ", "-");

            string uniqueSlug = baseSlug;
            int counter = 1;

            while (_context.Projects.Any(p => p.Slug == uniqueSlug))
            {
                uniqueSlug = $"{baseSlug}-{counter}";
                counter++;
            }

            var task = new ProjectTask
            {
                Name = request.Name,
                Slug = uniqueSlug,
                Description = request.Description,
                DueDate = request.DueDate,
                ProjectId = request.ProjectId,
                AssignedUserId = request.AssignedUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                StatusId = _status.DefaultStatusId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
    }
}
