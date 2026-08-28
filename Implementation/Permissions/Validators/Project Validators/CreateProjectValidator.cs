using Application;
using Application.Flowdesk.DTO.Projects;
using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;


namespace Implementation.Permissions.Validators.Project_Validators
{
    internal class CreateProjectValidator : CommonValidator<CreateProjectRequest>
    {
        private readonly IApplicationUser _currentUser;
        public CreateProjectValidator(FlowDbContext context, IApplicationUser currentUser) : base(context)
        {
            this._currentUser = currentUser;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    return !await _context.Projects.AnyAsync(p => p.Name == name, cancellationToken);
                })
                .WithMessage("Project name is already in use.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due date is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("Please select a valid team.")
                .MustAsync(async (teamId, cancellationToken) =>
                {
                    bool teamExists = await _context.Teams.AnyAsync(t => t.Id == teamId, cancellationToken);
                    if (!teamExists) return false;

                    bool canAssignAnyTeam = _currentUser.Permissions.Contains("Can Assign Teams");
                    if (canAssignAnyTeam) return true;

                    return await _context.UserTeams.AnyAsync(tm =>
                        tm.TeamId == teamId &&
                        tm.UserId == _currentUser.Id,
                        cancellationToken);
                })
                .WithMessage("Selected team is invalid or you do not have permission to assign projects to it.");
        }
    }
}
