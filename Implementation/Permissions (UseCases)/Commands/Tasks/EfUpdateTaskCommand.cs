using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Implementation.Permissions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Tasks
{
    public class EfUpdateTaskCommand : EfPermissions, IUpdateTaskCommand
    {
        private readonly IWebHostEnvironment _environment;

        public EfUpdateTaskCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IWebHostEnvironment environment) : base(context, defaultPermissionSettings.Value)
        {
            _environment = environment;
        }

        public int Id => _defaultPermissionSettings.EditTasksId;

        public string Name => _defaultPermissionSettings.EditTasksName;

        public void Execute(UpdateTaskRequest request)
        {
            var task = _context.Tasks
                .Include(t => t.Attachments)
                .FirstOrDefault(t => t.Slug == request.Slug);

            if (task == null)
            {
                throw new ArgumentException("Task not found");
            }

            task.Name = request.Name;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.ProjectId = request.ProjectId;
            task.AssignedUserId = request.AssignedUserId;
            task.UpdatedAt = DateTime.UtcNow;
            task.StatusId = request.StatusId;

            var rootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

            if (request.AttachmentIdsToDelete != null && request.AttachmentIdsToDelete.Any())
            {
                var attachmentsToDelete = task.Attachments
                    .Where(a => request.AttachmentIdsToDelete.Contains(a.Id))
                    .ToList();

                foreach (var attachment in attachmentsToDelete)
                {
                    var physicalPath = Path.Combine(rootPath, attachment.FilePath.TrimStart('/'));
                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }

                    _context.ProjectAttachments.Remove(attachment);
                }
            }

            if (request.NewAttachments != null && request.NewAttachments.Any())
            {
                var uploadsFolder = Path.Combine(rootPath, "uploads", "tasks");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in request.NewAttachments)
                {
                    if (file.Length == 0) continue;

                    var fileExtension = Path.GetExtension(file.FileName);
                    var storedFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var fullPhysicalPath = Path.Combine(uploadsFolder, storedFileName);

                    using (var stream = new FileStream(fullPhysicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    task.Attachments.Add(new ProjectAttachment
                    {
                        OriginalFileName = file.FileName,
                        FilePath = $"/uploads/tasks/{storedFileName}",
                        FileSize = file.Length
                    });
                }
            }

            _context.SaveChanges();
        }
    }
}
