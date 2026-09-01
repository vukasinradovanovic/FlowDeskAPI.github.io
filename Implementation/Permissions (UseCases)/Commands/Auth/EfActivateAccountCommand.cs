using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.Exceptions;
using Application.Flowdesk.Interfaces;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using Implementation.Emails;
using Implementation.Emails.Enums;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Auth
{
    public class EfActivateAccountCommand : EfPermissions, IActivateAccountCommand
    {
        private IEmailSender _sender;
        private EmailTemplateComposer _composer;

        public EfActivateAccountCommand(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public void Execute(string request)
        {
            var user = _context.Users.FirstOrDefault(x => x.ActivationCode == request);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User));
            }

            if (user.ActivatedAt.HasValue)
            {
                throw new EntityNotFoundException(nameof(User));
            }

            if ((DateTime.UtcNow - user.RegisteredAt.Value).TotalMinutes > 5)
            {
                throw new EntityNotFoundException(nameof(User));
            }

            user.ActivatedAt = DateTime.UtcNow;
            user.ActivationCode = null;

            var html = _composer.GetTemplateContent(EmailTemplate.Activation, user);
            _sender.SendEmail(user.Email, "Account activated", html);
        }
    }
}
