namespace Catalog.API
{
    public record PaginationDTO(int PageNumber, int PageSize, string SortBy, bool IsDesc);
    public record PagedListDTO<T>(List<T> Data, PageDTO Page);
    public record PageDTO(int PageNumber, int PageSize, int TotalPages, int RecordsInPage, int TotalRecords, string SortedBy, bool IsDesc);

    public record CategoryDTO(string CategoryId, string CategoryName, string CategoryDescription);
    public record CategoryAddUpdateDTO(string CategoryName, string CategoryDescription);

    public record ProductDTO(string ProductId, string ProductName, string CategoryName, string CategoryId, string Description, decimal Price, decimal Mrp, decimal Discount, decimal Height, decimal Depth, decimal Width, string? Image);
    public record ProductAddUpdateDTO(string ProductName, string CategoryId, string Description, decimal Price, decimal Mrp, decimal Discount, decimal Height, decimal Depth, decimal Width, string? Image);
    public record ProductSummaryDTO(string ProductId, string ProductName, decimal Price, string CategoryId, string CategoryName, string Description);
}
