namespace Catalog.Models
{
    public class ProductSummaryModel
    {
        public string ProductId { get; set; }


        public string ProductName { get; set; }

        public string CategoryName { get; set; }
        
        public string CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public byte[]? Image { get; set; }
        public ProductSummaryModel(string productId, string productName, decimal price, string categoryId, string categoryName, string description = "")
        {
            ProductId = productId;
            ProductName = productName;
            CategoryName = categoryName;
            CategoryId = categoryId;
            Description = description;
            Price = price;
        }
    }
}
