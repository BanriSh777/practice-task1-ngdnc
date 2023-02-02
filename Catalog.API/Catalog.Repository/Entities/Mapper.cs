using Catalog.Models;

namespace Catalog.Repository.Entities
{
    public static class Mapper
    {
        public static CategoryModel ToModel(this Category category)
        {
            CategoryModel categoryModel = new()
            {
                CategoryDescription = category.CategoryDescription ?? "",
                CategoryName = category.CategoryName,
                CategoryId = category.CategoryId,
            };
            return categoryModel;
        }

        public static ProductModel ToModel(this Product product)
        {
            return new ProductModel(product.ProductId,
                product.ProductName,
                product.Price,
                product.CategoryId!,
                product.Category!.CategoryName)
            {
                Description = product.Description ?? "",
                Mrp = product.Mrp,
                Discount = product.Discount??0,
                Depth = product.Depth ??0,
                Width = product.Width ??0,
                Height = product.Height ?? 0,
                Image = product.Image,
            };
        }

        public static ProductSummaryModel ToSummaryModel(this Product product)
        {
            return new ProductSummaryModel(product.ProductId,
                product.ProductName,
                product.Price,
                product.CategoryId!,
                product.Category!.CategoryName)
            {
                Description = product.Description ?? "",
                Image = product.Image,
            };
        }

        public static Category ToEntity(this CategoryAddUpdateModel categoryAddUpdate, string id)
        {

            return new Category
            {
                CategoryId = id,
                CategoryName = categoryAddUpdate.CategoryName,
                CategoryDescription = categoryAddUpdate.CategoryDescription
            };
        }
        
        public static Product ToEntity(this ProductAddUpdateModel productAddUpdate, string id)
        {

            return new Product
            {
                ProductId = id,
                CategoryId = productAddUpdate.CategoryId,
                Depth = productAddUpdate.Depth,
                Height = productAddUpdate.Height,
                Image = productAddUpdate.Image,
                Mrp = productAddUpdate.Mrp,
                Price = productAddUpdate.Price,
                ProductName = productAddUpdate.ProductName,
                Width = productAddUpdate.Width,
                Description = productAddUpdate.Description,
                Discount = productAddUpdate.Discount

            };
        }
    }
}
