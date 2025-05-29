namespace WebApi.Service
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string? SortBy { get; set; }
        public bool SortOrder { get; set; } = false;

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
