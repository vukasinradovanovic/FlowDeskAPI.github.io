using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions.Validators.Project_Validators
{
    public class DeleteProjectValidator : CommonValidator<string>
    {
        public DeleteProjectValidator(FlowDbContext context) : base(context)
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Project slug must not be empty.")
                .MustAsync(async (slug, cancellation) =>
                {
                    var projectExists = await _context.Projects.AnyAsync(p => p.Slug == slug, cancellation);
                    return projectExists;
                }).WithMessage("Project with the specified slug does not exist.");
        }
    }
}
