using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Implementation.Permissions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Tasks
{
    public class EfCreateTaskCommand : EfPermissions, ICreateTaskCommand
    {
        private readonly StatusSettings _status;
        private readonly IWebHostEnvironment _environment;

        public EfCreateTaskCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IOptions<StatusSettings> statusSettings, IWebHostEnvironment environment) : base(context, defaultPermissionSettings.Value)
        {
            _status = statusSettings.Value;
            _environment = environment;
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
            if (request.Attachments != null && request.Attachments.Any())
            {
                var rootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
                var uploadsFolder = Path.Combine(rootPath, "uploads", "tasks");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var attachmentEntities = new List<ProjectAttachment>();

                foreach (var file in request.Attachments)
                {
                    if (file.Length == 0) continue;

                    var fileExtension = Path.GetExtension(file.FileName);
                    var storedFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var fullPhysicalPath = Path.Combine(uploadsFolder, storedFileName);

                    // FIX: Use synchronous CopyTo since Execute is a synchronous method
                    using (var stream = new FileStream(fullPhysicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream); // Removed async method without await
                    }

                    attachmentEntities.Add(new ProjectAttachment
                    {
                        TaskId = task.Id,
                        OriginalFileName = file.FileName,
                        FilePath = $"/uploads/tasks/{storedFileName}",
                        FileSize = file.Length
                    });
                }

                if (attachmentEntities.Any())
                {
                    _context.ProjectAttachments.AddRange(attachmentEntities);
                    _context.SaveChanges(); // FIX: Changed from SaveChangesAsync to SaveChanges
                }
            }
        }
    }
}
