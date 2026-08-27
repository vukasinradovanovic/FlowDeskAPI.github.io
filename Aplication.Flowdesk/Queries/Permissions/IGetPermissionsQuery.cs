using Application.Flowdesk.DTO.Permissions;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Permissions
{
    public interface IGetPermissionsQuery : IQuery<object?, IEnumerable<PermissionsResponse>>
    {
    }
}
