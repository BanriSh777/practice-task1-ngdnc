namespace Catalog.Repository.Entities;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal Mrp { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Height { get; set; }

    public decimal? Depth { get; set; }

    public decimal? Width { get; set; }

    public byte[]? Image { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();
}
