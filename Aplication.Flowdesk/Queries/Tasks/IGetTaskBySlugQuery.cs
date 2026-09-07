using Application.Flowdesk.DTO.Tasks;
using Application.Flowdesk.Interfaces;

namespace Application.Flowdesk.Queries.Tasks
{
    public interface IGetTaskBySlugQuery : IQuery<string, TaskResponse>
    {
    }
}
