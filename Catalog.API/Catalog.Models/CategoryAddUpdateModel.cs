namespace Catalog.Models
{
    public class CategoryAddUpdateModel
    {
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public CategoryAddUpdateModel(string categoryName, string categoryDescription)
        {
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
        }
    }
}
