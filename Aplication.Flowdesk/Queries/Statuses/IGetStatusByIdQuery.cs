using Application.Flowdesk.DTO.Statuses;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Statuses
{
    public interface IGetStatusByIdQuery : IQuery<int, StatusResponse>
    {
    }
}
