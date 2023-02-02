namespace Catalog.Models
{
    public class CategoryModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public CategoryModel(string categoryId, string categoryName, string categoryDescription = "")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
        }

        public CategoryModel()
        {
        }
    }
}
