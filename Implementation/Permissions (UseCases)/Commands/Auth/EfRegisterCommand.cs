using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.DTO.Auth;
using Application.Flowdesk.Interfaces;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using Implementation.Emails;
using Implementation.Emails.Enums;
using Implementation.Permissions.Validators;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Auth
{
    public class EfRegisterCommand : EfPermissions, IRegisterUserCommand
    {
        private readonly RoleSettings _roleSettings;
        private readonly IEmailSender _emailSender;
        private readonly EmailTemplateComposer _composer;

        public EfRegisterCommand(FlowDbContext context,
                                 RegisterUserValidator validator,
                                 IOptions<RoleSettings> roleSettings,
                                 IOptions<DefaultPermissionSettings> defaultPermissionSettings,
                                 IEmailSender emailSender,
                                 EmailTemplateComposer composer)
                            : base(context, defaultPermissionSettings.Value)
        {
            _roleSettings = roleSettings.Value;
            _emailSender = emailSender;
            _composer = composer;
        }

        public string Name => _defaultPermissionSettings.GuestPermissionName;
        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public void Execute(RegisterRequest request)
        {
            var activationCode = Guid.NewGuid().ToString("N");

            User user = new User
            {
                Username = request.Username.Trim(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                AvatarColor = request.AvatarColor,
                ActivationCode = activationCode,
                RegisteredAt = DateTime.UtcNow
            };

            UserRole userRole = new UserRole
            {
                User = user,
                RoleId = _roleSettings.DefaultRoleId
            };

            UserRolePermission userRolePermissionGuest = new UserRolePermission
            {
                UserRole = userRole,
                PermissionId = _defaultPermissionSettings.GuestPermissionId
            };

            UserRolePermission userRolePermissionViewUserProjects = new UserRolePermission
            {
                UserRole = userRole,
                PermissionId = _defaultPermissionSettings.ViewUserProjectsId
            };

            _context.Users.Add(user);
            _context.UserRoles.Add(userRole);
            _context.UserRolePermissions.AddRange(userRolePermissionGuest, userRolePermissionViewUserProjects);

            var templateModel = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                ActivationUrl = $"https://localhost:7175/api/ActivateAccount/{activationCode}"
            };

            var html = _composer.GetTemplateContent(EmailTemplate.Activation, templateModel);

            _emailSender.SendEmail(user.Email, "Aktivacija Flowdesk naloga", html);

            _context.SaveChanges();
        }
    }
}