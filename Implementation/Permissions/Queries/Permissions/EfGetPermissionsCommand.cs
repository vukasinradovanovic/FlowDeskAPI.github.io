using Application.Flowdesk.DTO.Permissions;
using Application.Flowdesk.Queries.Permissions;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Permissions
{
    public class EfGetPermissionsCommand : EfPermissions, IGetPermissionsQuery
    {
        public EfGetPermissionsCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public IEnumerable<PermissionsResponse> Execute(object? request)
        {
            var permissions = _context.Permissions.Select(p => new PermissionsResponse
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return permissions;
        }
    }
}
