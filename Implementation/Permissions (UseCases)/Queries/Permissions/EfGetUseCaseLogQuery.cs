using Application.Flowdesk.DTO.Pagination;
using Application.Flowdesk.DTO.Permissions;
using Application.Flowdesk.Extentions;
using Application.Flowdesk.Queries.Permissions;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Implementation.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Implementation.Permissions__UseCases_.Queries.Permissions
{
    public class EfGetUseCaseLogQuery : EfPermissions, IGetUseCaseLogQuery
    {
        public EfGetUseCaseLogQuery(FlowDbContext context, IOptions<DefaultPermissionSettings> defaultPermissionSettings) : base(context, defaultPermissionSettings.Value)
        {
        }

        public int Id => _defaultPermissionSettings.CanViewUseCaseLogsId;

        public string Name => _defaultPermissionSettings.CanViewUseCaseLogsName;

        public PagedResponse<UseCaseLogResponse> Execute(PagedRequest? request)
        {
            var query = _context.UseCaseLogs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request?.Keyword))
            {
                var keyword = request.Keyword.Trim();
                query = query.Where(q => q.UseCaseName.Contains(keyword));
            }

            return query
                .OrderByDescending(q => q.CreatedAt)
                .Paginate(request, q => new UseCaseLogResponse
                {
                    Id = q.Id,
                    Username = q.Username,
                    CreatedAt = q.CreatedAt,
                    Action = q.UseCaseName,
                    RawData = q.UseCaseData,
                    IsSuccess = q.IsSuccessfull
                });
        }
    }
}
