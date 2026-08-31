using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Projects
{
    public class EfDeleteProjectCommand : EfPermissions, IDeleteProjectCommand
    {
        public EfDeleteProjectCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.DeleteProjectsId;

        public string Name => _defaultPermissionSettings.DeleteProjectsName;

        public void Execute(string request)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Slug == request);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
