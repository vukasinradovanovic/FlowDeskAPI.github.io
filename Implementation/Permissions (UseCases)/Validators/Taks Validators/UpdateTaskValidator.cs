using Application.Flowdesk.DTO.Tasks;
using DataAccess.FlowDesk;
using FluentValidation;
using Implementation.Permissions.Validators.Common;

namespace Implementation.Permissions__UseCases_.Validators.Taks_Validators
{
    public class UpdateTaskValidator : CommonValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator(FlowDbContext context) : base(context)
        {
            RuleFor(x => x.Slug)
                .NotEmpty()
                .WithMessage("Task slug is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Task name is required.")
                .MaximumLength(200)
                .WithMessage("Task name cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description cannot be null.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.MinValue)
                .WithMessage("Valid due date is required.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("A valid project must be selected.");

            RuleFor(x => x.AssignedUserId)
                .GreaterThan(0)
                .WithMessage("A valid assigned user must be selected.");

            RuleFor(x => x.StatusId)
                .GreaterThan(0)
                .WithMessage("A valid status must be selected.");

            RuleFor(x => x.StatusId)
            .GreaterThan(0)
            .Must(statusId => _context.Statuses.Any(s => s.Id == statusId))
            .WithMessage("The selected status does not exist.");

            RuleForEach(x => x.NewAttachments)
                .Must(file => file.Length <= 10 * 1024 * 1024)
                .WithMessage("Individual file size cannot exceed 10 MB.");
        }
    }
}
