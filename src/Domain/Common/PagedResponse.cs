namespace Budgetly.Domain.Common;

public class PagedResponse<T> : BaseResponse<T> where T : class
{
    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecordCount) : base(data)
    {
        CurrentPage = pageNumber >= 1 ? pageNumber : 1;
        PageSize = pageSize >= 1 ? pageSize : 1;
        TotalPageCount = (int)Math.Ceiling(totalRecordCount / (double)PageSize);
        TotalRecordCount = totalRecordCount;
    }

    public int? CurrentPage { get; private set; }
    public int? TotalPageCount { get; private set; }
    public int? TotalRecordCount { get; private set; }
    public int? PageSize { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPageCount;
}