using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Statuses
{
    public interface IGetAllStatusesQuery : IQuery<int?, IEnumerable<StatusResponse>>
    {
    }
}
