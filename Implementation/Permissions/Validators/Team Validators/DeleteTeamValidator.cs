using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions.Validators.Team_Validators
{
    public class DeleteTeamValidator : CommonValidator<int>
    {
        public DeleteTeamValidator(FlowDbContext context) : base(context)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(id => id)
                .GreaterThan(0).WithMessage("Invalid team ID.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    // 1. Check if the team exists
                    return await _context.Teams.AnyAsync(t => t.Id == id, cancellationToken);
                })
                .WithMessage("Team does not exist.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    bool hasMembers = await _context.Teams
                        .AnyAsync(t => t.Id == id && t.Members.Any(), cancellationToken);

                    return !hasMembers;
                })
                .WithMessage("Cannot delete a team that still has assigned members.");
        }
    }
}
