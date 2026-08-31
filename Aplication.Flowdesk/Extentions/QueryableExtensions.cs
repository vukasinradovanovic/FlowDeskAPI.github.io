using Application.Flowdesk.DTO.Pagination;
using System.Linq.Expressions;

namespace Application.Flowdesk.Extentions
{
    public static class QueryableExtensions
    {
        public static PagedResponse<TDto> Paginate<TEntity, TDto>(
            this IQueryable<TEntity> query,
            PagedRequest? search,
            Expression<Func<TEntity, TDto>> transform)
            where TDto : class
        {
            search ??= new PagedRequest();

            int currentPage = (search.Page.HasValue && search.Page.Value > 0) ? search.Page.Value : 1;
            int perPage = (search.PerPage.HasValue && search.PerPage.Value > 0) ? search.PerPage.Value : 10;

            var totalCount = query.Count();

            var items = query
                .Skip((currentPage - 1) * perPage)
                .Take(perPage)
                .Select(transform)
                .ToList();

            return new PagedResponse<TDto>
            {
                CurrentPage = currentPage,
                PerPage = perPage,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
