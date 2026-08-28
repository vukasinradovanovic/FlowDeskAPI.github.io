using Application.Flowdesk.DTO.CreateTeamRequest;
using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions.Validators.Team_Validators
{
    public class CreateTeamValidator : CommonValidator<CreateTeamRequest>
    {
        public CreateTeamValidator(FlowDbContext context) : base(context)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(100).WithMessage("Team name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    return !await _context.Teams.AnyAsync(t => t.Name == name, cancellationToken);
                })
                .WithMessage("Team name is already in use.");
        }
    }
}
