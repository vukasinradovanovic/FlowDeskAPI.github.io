using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.Exceptions;
using Application.Flowdesk.Interfaces;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using Implementation.Emails;
using Implementation.Permissions;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Commands.Auth
{
    public class EfActivateAccountCommand : EfPermissions, IActivateAccountCommand
    {
        private readonly IEmailSender _sender;
        private readonly EmailTemplateComposer _composer;

        public EfActivateAccountCommand(FlowDbContext context,
                                        IOptions<DefaultPermissionSettings> defaultPermissionSettings,
                                        IEmailSender sender,
                                        EmailTemplateComposer composer)
            : base(context, defaultPermissionSettings.Value)
        {
            _sender = sender;
            _composer = composer;
        }

        public int Id => _defaultPermissionSettings.GuestPermissionId;
        public string Name => _defaultPermissionSettings.GuestPermissionName;

        public void Execute(string request)
        {
            var user = _context.Users.FirstOrDefault(x => x.ActivationCode == request);

            if (user == null || user.ActivatedAt.HasValue)
            {
                throw new EntityNotFoundException(nameof(User));
            }

            if (!user.RegisteredAt.HasValue || (DateTime.UtcNow - user.RegisteredAt.Value).TotalMinutes > 15)
            {
                throw new InvalidOperationException("Aktivacioni kod je istekao.");
            }

            user.ActivatedAt = DateTime.UtcNow;
            user.ActivationCode = string.Empty;

            _context.SaveChanges();
        }
    }
}