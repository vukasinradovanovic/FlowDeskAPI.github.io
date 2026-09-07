using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Auth;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using FlowDeskAPI.DTO.Autentification;
using Implementation.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Queries.Auth
{
    public class EfGetAllUsersQuery : EfPermissions, IGetAllUsersQuery
    {
        public EfGetAllUsersQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.CanAssignTeamsId;

        public string Name => _defaultPermissionSettings.CanAssignTeamsName;

        public PagedResponse<UserResponse> Execute(PagedRequest? request)
        {
            var users = _context.Users.AsNoTracking();
            return users.Paginate(request, u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                AvatarColor = u.AvatarColor,
            });
        }
    }
}
