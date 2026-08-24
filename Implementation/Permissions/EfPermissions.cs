using DataAccess.FlowDesk;

namespace Implementation.Permissions
{
    public class EfPermissions
    {
        protected readonly FlowDbContext _context;

        protected EfPermissions(FlowDbContext context)
        {
            _context = context;
        }
    }
}
