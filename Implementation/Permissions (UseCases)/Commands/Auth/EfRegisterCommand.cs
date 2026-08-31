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

        public EfRegisterCommand(FlowDbContext context,
                                 RegisterUserValidator validator,
                                 IOptions<RoleSettings> roleSettings,
                                 IOptions<DefaultPermissionSettings> defaultPermissionSettings)
                            : base(context, defaultPermissionSettings.Value)
        {
            _roleSettings = roleSettings.Value;
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
            _context.SaveChanges();
        }
    }
}
