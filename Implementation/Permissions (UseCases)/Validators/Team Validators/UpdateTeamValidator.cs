using Application.Flowdesk.DTO.Teams;
using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions.Validators.Team_Validators
{
    public class UpdateTeamValidator : CommonValidator<UpdateTeamRequest>
    {
        public UpdateTeamValidator(FlowDbContext context) : base(context)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid team ID.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _context.Teams.AnyAsync(t => t.Id == id, cancellationToken);
                })
                .WithMessage("Team with the specified ID does not exist.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(100).WithMessage("Team name cannot exceed 100 characters.");

            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                {
                    bool nameTakenByAnotherTeam = await _context.Teams
                        .AnyAsync(t => t.Name == request.Name && t.Id != request.Id, cancellationToken);

                    return !nameTakenByAnotherTeam;
                })
                .WithMessage("Team name is already in use by another team.");
        }
    }
}
