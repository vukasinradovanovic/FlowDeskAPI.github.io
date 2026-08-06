using Application.Flowdesk.DTO.Auth;
using DataAccess.FlowDesk;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterRequest>
    {
        private readonly FlowDbContext _context;

        public RegisterUserValidator(FlowDbContext context)
        {
            _context = context;

            this.RuleLevelCascadeMode = CascadeMode.Stop;

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
                    var normalizedEmail = email.Trim().ToLower();
                    bool exists = await _context.Users.AnyAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
                    return !exists;
                })
                .WithMessage("Email address is already in use.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.AvatarColor)
                .Must(color => string.IsNullOrEmpty(color) || System.Enum.TryParse<AvatarColor>(color, true, out _))
                .WithMessage("Avatar color must be one of the following: emerald, indigo, amber, rose.");

        }
    }
}
