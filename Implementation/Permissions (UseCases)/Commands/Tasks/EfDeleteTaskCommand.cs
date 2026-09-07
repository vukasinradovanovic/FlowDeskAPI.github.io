using Application.Flowdesk.Commands.Tasks;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Tasks
{
    public class EfDeleteTaskCommand : EfPermissions, IDeleteTaskCommand
    {
        public EfDeleteTaskCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.DeleteTasksId;

        public string Name => _defaultPermissionSettings.DeleteTasksName;

        public void Execute(string request)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Slug == request);

            if (task == null)
            {
                throw new Exception($"Task with slug {request} does not exist.");
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
