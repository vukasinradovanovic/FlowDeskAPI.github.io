using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Projects
{
    public class EfCreateProjectCommand : EfPermissions, ICreateProjectCommand
    {
        private readonly StatusSettings _status;
        public EfCreateProjectCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings, IOptions<StatusSettings> status) : base(context, defaultPermissionSettings.Value)
        {
            _status = status.Value;
        }

        public int Id => _defaultPermissionSettings.CreateProjectsId;
        public string Name => _defaultPermissionSettings.CreateProjectsName;

        public void Execute(CreateProjectRequest request)
        {
            string baseSlug = request.Name.Trim().ToLower().Replace(" ", "-");

            string uniqueSlug = baseSlug;
            int counter = 1;

            while (_context.Projects.Any(p => p.Slug == uniqueSlug))
            {
                uniqueSlug = $"{baseSlug}-{counter}";
                counter++;
            }

            var project = new Project
            {
                Name = request.Name,
                Slug = uniqueSlug,
                Icon = request.Icon,
                Theme = request.Theme,
                DueDate = request.DueDate,
                StatusId = _status.DefaultStatusId,
                CreatedAt = DateTime.UtcNow,
                ProjectTeams = new List<ProjectTeam>
                {
                    new ProjectTeam { TeamId = request.TeamId }
                }
            };

            _context.Projects.Add(project);
            _context.SaveChanges();
        }
    }
}
