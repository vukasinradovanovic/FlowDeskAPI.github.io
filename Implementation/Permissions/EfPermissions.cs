using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;

namespace Implementation.Permissions
{
    public class EfPermissions
    {
        protected readonly FlowDbContext _context;
        protected readonly DefaultPermissionSettings _defaultPermissionSettings;

        public EfPermissions(FlowDbContext context, DefaultPermissionSettings defaultPermissionSettings)
        {
            _context = context;
            _defaultPermissionSettings = defaultPermissionSettings;
        }
    }
}
