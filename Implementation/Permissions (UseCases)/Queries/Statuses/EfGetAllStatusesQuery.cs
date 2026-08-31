using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Queries.Statuses;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Queries.Statuses
{
    public class EfGetAllStatusesQuery : EfPermissions, IGetAllStatusesQuery
    {
        public EfGetAllStatusesQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }
        public int Id => _defaultPermissionSettings.GuestPermissionId;
        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public IEnumerable<StatusResponse> Execute(int? request)
        {
            return _context.Statuses
                .Select(s => new StatusResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Theme = s.StatusTheme
                })
                .ToList();
        }
    }
}
