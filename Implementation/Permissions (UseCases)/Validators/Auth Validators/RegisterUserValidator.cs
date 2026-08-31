using Application.Flowdesk.DTO.Auth;
using DataAccess.FlowDesk;
using Domain.Enums;
using FluentValidation;
using Implementation.Permissions.Validators.Common;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Permissions.Validators
{
    public class RegisterUserValidator : CommonValidator<RegisterRequest>
    {
        public RegisterUserValidator(FlowDbContext context) : base(context)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Matches(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$")
                .WithMessage("Username can only contain letters, numbers, spaces, hyphens, and underscores.")
                .MustAsync(async (username, cancellationToken) =>
                {
                    return !await _context.Users.AnyAsync(u => u.Username == username, cancellationToken);
                })
                .WithMessage("Username is in use.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email address is not in a valid format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .MustAsync(async (email, cancellationToken) =>
                {
                    var normalizedEmail = email.Trim();
                    return !await _context.Users.AnyAsync(u => u.Email == normalizedEmail, cancellationToken);
                })
                .WithMessage("Email address is already in use.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.AvatarColor)
                .Must(color => string.IsNullOrEmpty(color) || Enum.TryParse<AvatarColor>(color, true, out _))
                .WithMessage("Avatar color must be one of the following: emerald, indigo, amber, rose.");
        }
    }
}
