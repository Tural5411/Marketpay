namespace MarketPay.Domain.Common;

public class PaginationRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : (value > 100 ? 100 : value);
    }

    public int Skip => (PageNumber - 1) * PageSize;
}

public class ProductPaginationRequest : PaginationRequest
{
    public ProductSortBy SortBy { get; set; } = ProductSortBy.Id;
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    public string? SearchTerm { get; set; }
    public string? Category { get; set; }
    public bool? IsActive { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

