using Application.Flowdesk.Commands.Projects;
using Application.Flowdesk.DTO.Projects;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.ProjectTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Projects
{
    public class EfUpdateProjectCommand : EfPermissions, IUpdateProjectCommand
    {
        public EfUpdateProjectCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.EditProjectsId;

        public string Name => _defaultPermissionSettings.EditProjectsName;

        public void Execute(UpdateProjectRequest request)
        {
            var project = _context.Projects
                .Include(p => p.ProjectTeams)
                .FirstOrDefault(p => p.Slug == request.Slug);

            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            project.Name = request.Name;
            project.Slug = request.Slug;
            project.Icon = request.Icon;
            project.Theme = request.Theme;
            project.DueDate = request.DueDate;
            project.StatusId = request.StatusId;

            var existingProjectTeam = project.ProjectTeams.FirstOrDefault();

            if (existingProjectTeam != null)
            {
                existingProjectTeam.TeamId = request.TeamId;
            }
            else
            {
                project.ProjectTeams.Add(new ProjectTeam
                {
                    TeamId = request.TeamId
                });
            }

            _context.SaveChanges();
        }
    }
}
