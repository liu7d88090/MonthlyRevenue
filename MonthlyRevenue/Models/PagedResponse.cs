namespace MonthlyRevenue.Models
{
    public sealed class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 100;
        public int TotalCount { get; init; }
    }
}
