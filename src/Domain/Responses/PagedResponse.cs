namespace Budgetly.Domain.Responses;

public class PagedResponse<T> : Response<T> where T : class
{
    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalResults) : base(data)
    {
        PageNumber = pageNumber >= 1 ? pageNumber : 1;
        PageSize = pageSize >= 1 ? pageSize : 10;
        TotalPages = (int)Math.Ceiling(totalResults / (double)PageSize);
        TotalResults = totalResults;
    }

    public int? PageNumber { get; private set; }
    public int? TotalPages { get; private set; }
    public int? TotalResults { get; private set; }
    public int? PageSize { get; private set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}