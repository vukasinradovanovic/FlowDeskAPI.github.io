using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Queries.Statuses;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Queries.Statuses
{
    public class EfGetStatusByIdQuery : EfPermissions, IGetStatusByIdQuery
    {
        public EfGetStatusByIdQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public StatusResponse Execute(int request)
        {
            var status = _context.Statuses.Find(request);
            return new StatusResponse
            {
                Id = status.Id,
                Name = status.Name,
                Theme = status.StatusTheme
            };
        }
    }
}
