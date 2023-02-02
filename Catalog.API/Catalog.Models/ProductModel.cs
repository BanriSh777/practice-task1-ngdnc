namespace Catalog.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Mrp { get; set; }

        public decimal Discount { get; set; }

        public decimal Height { get; set; }

        public decimal Depth { get; set; }

        public decimal Width { get; set; }

        public byte[]? Image { get; set; }
        public ProductModel(string id, string name, decimal price, string categoryId, string categoryName, string description = "", byte[]? image = null)
        {
            ProductId = id;
            ProductName = name;
            Price = price;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
            Image = image;
        }
    }
}
