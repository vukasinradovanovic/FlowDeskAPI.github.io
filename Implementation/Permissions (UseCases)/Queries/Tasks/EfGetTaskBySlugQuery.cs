using Application.Flowdesk.DTO.Attachments;
using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Queries.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Queries.Tasks
{
    public class EfGetTaskBySlugQuery : EfPermissions, IGetTaskBySlugQuery
    {
        public EfGetTaskBySlugQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public TaskResponse Execute(string request)
        {
            return _context.Tasks
                .Where(t => t.Slug == request)
                .Select(t => new TaskResponse
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
                    },
                    Attachments = t.Attachments.Select(a => new AttachmentResponse
                    {
                        Id = a.Id,
                        OriginalFileName = a.OriginalFileName,
                        FilePath = a.FilePath,
                        FileSize = a.FileSize,
                        UploadedAt = a.UploadedAt
                    })
                })
                .FirstOrDefault() ?? throw new ArgumentException("Project not found");
        }
    }
}
