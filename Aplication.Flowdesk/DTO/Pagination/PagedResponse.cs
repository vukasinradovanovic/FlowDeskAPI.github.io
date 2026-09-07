namespace Application.Flowdesk.DTO.Pagination
{
    public class PagedResponse<TDto> where TDto : class
    {
        public int TotalCount { get; set; }
        public int PagesCount => (int)Math.Ceiling((decimal)TotalCount / PerPage);
        public IEnumerable<TDto> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
    }

    public class PagedRequest
    {
        public string? Keyword { get; set; }
        public int? currentPage { get; set; } = 1;
        public int? PerPage { get; set; } = 10;
    }
}
