using Application.Flowdesk.Commands.Auth;
using Application.Flowdesk.DTO.Auth;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using Implementation.Permissions.Validators;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions.Commands.Auth
{
    public class EfRegisterCommand : EfPermissions, IRegisterUserCommand
    {
        private readonly RoleSettings _roleSettings;
        private readonly DefaultPermissionSettings _defaultPermissionSettings;

        public EfRegisterCommand(FlowDbContext context,
                                 RegisterUserValidator validator,
                                 IOptions<RoleSettings> roleSettings,
                                 IOptions<DefaultPermissionSettings> defaultPermissionSettings)
                            : base(context)
        {
            _roleSettings = roleSettings.Value;
            _defaultPermissionSettings = defaultPermissionSettings.Value;
        }

        public string Name => _defaultPermissionSettings.GuestPermissionName;
        public int Id => _defaultPermissionSettings.GuestPermissionId;

        public void Execute(RegisterRequest request)
        {

            User user = new User
            {
                Username = request.Username.Trim(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                AvatarColor = request.AvatarColor
            };
            UserRole userRole = new UserRole
            {
                User = user,
                RoleId = _roleSettings.DefaultRoleId
            };
            UserRolePermission userRolePermission = new UserRolePermission
            {
                UserRole = userRole,
                PermissionId = _defaultPermissionSettings.GuestPermissionId
            };

            _context.Users.Add(user);
            _context.UserRoles.Add(userRole);
            _context.UserRolePermissions.Add(userRolePermission);
            _context.SaveChanges();
        }
    }
}
