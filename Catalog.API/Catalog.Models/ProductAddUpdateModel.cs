namespace Catalog.Models
{
    public class ProductAddUpdateModel
    {
        public string ProductName { get; set; }

        public string CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Mrp { get; set; }

        public decimal Discount { get; set; }

        public decimal Height { get; set; }

        public decimal Depth { get; set; }

        public decimal Width { get; set; }

        public byte[]? Image { get; set; }

        public ProductAddUpdateModel(string Name, string categoryId, decimal price, string ProdDescription = "")
        {
            ProductName = Name;
            CategoryId = categoryId;
            Description = ProdDescription;
            Price = price;
            Mrp = price;
        }
    }
}
