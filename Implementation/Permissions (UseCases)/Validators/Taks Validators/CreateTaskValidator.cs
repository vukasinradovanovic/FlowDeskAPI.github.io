using Application.Flowdesk.DTO.Tasks;
using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions__UseCases_.Validators.Taks_Validators
{
    public class CreateTaskValidator : CommonValidator<CreateTaskRequest>
    {
        public CreateTaskValidator(FlowDbContext context) : base(context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Task name is required.");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Task description cannot exceed 1000 characters.");

            RuleFor(x => x.DueDate)
               .NotEmpty().WithMessage("Due date is required.")
               .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("Please select a valid project.")
                .MustAsync(async (projectId, cancellationToken) =>
                {
                    return await _context.Projects.AnyAsync(p => p.Id == projectId, cancellationToken);
                })
                .WithMessage("Selected project does not exist.");

            RuleFor(x => x.AssignedUserId)
                .GreaterThan(0).WithMessage("Please select a valid user.")
                .MustAsync(async (assignedUserId, cancellationToken) =>
                {
                    return await _context.Users.AnyAsync(u => u.Id == assignedUserId, cancellationToken);
                })
                .WithMessage("Selected user does not exist.");


        }
    }
}
