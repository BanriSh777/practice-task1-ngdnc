using Catalog.Models;
using Catalog.Repository.Entities;
using Catalog.Repository.Sorters;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Catalog.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogdbContext _catalogContext;

        public ProductRepository(CatalogdbContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<string> NextId()
        {
            var nextId = await _catalogContext.Products.MaxAsync(p => p.ProductId);

            if (string.IsNullOrEmpty(nextId)) return "PRO000001";

            int lastIdNumber = int.Parse(Regex.Match(nextId, @"\d+").Value);
            return "PRO" + (++lastIdNumber).ToString().PadLeft(6, '0');

        }

        public async Task<ProductSummaryModel?> Add(ProductAddUpdateModel productAdd)
        {
            var category = await _catalogContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == productAdd.CategoryId);
            if (category == null) return null;
            var id = await NextId();
            var newProduct = productAdd.ToEntity(id);
            await _catalogContext.Products.AddAsync(newProduct);
            await _catalogContext.SaveChangesAsync();
            return newProduct.ToSummaryModel();
        }

        public async Task<ProductModel?> Delete(string id)
        {
            var product = await _catalogContext.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return null;
            _catalogContext.Products.Remove(product);
            await _catalogContext.SaveChangesAsync();
            return product.ToModel();
        }

        public async Task<PagedListModel<ProductSummaryModel>> Get(PaginationationModel paginationationModel)
        {
            var products = await _catalogContext.Products.Include(p => p.Category).CustomSortIntoPages(paginationationModel, out PageModel pageModel).ToListAsync();
            var productsPages = new PagedListModel<ProductSummaryModel>(
                    products.Select(p => p.ToSummaryModel()).ToList(),
                    pageModel);
            return productsPages;
        }

        public async Task<PagedListModel<ProductModel>?> Get(string categoryId, PaginationationModel paginationationModel)
        {
            var products = await _catalogContext.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).CustomSortIntoPages(paginationationModel, out PageModel pageModel).ToListAsync();
            var productsPages = new PagedListModel<ProductModel>(
                    products.Select(p => p.ToModel()).ToList(),
                    pageModel);
            return productsPages;
        }

        public async Task<ProductModel?> Get(string id)
        {
            return (await this._catalogContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id))?.ToModel();
        }

        public async Task<PagedListModel<ProductSummaryModel>> Search(string searchKey, PaginationationModel paginationationModel)
        {
            var products = await _catalogContext.Products
                .Include(p => p.Category)
                .Where(p => p.ProductId.ToLower().Contains(searchKey) || p.ProductName.ToLower().Contains(searchKey))
                .CustomSortIntoPages(paginationationModel, out PageModel pageModel)
                .Select(p => p.ToSummaryModel())
                .ToListAsync();
            return new PagedListModel<ProductSummaryModel> (products, pageModel);
        }

        public async Task<ProductModel?> Update(string id, ProductAddUpdateModel productUpdate)
        {
            var toBeUpdated = await _catalogContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            if (toBeUpdated == null) return null;

            var category = await _catalogContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == productUpdate.CategoryId);
            if (category == null)
            {
                toBeUpdated.CategoryId = "INVALIDCATEGORY" + toBeUpdated.CategoryId;
                return toBeUpdated.ToModel();
            }

            toBeUpdated.Mrp = productUpdate.Mrp;
            toBeUpdated.CategoryId = productUpdate.CategoryId;
            toBeUpdated.Price = productUpdate.Price;
            toBeUpdated.Discount = productUpdate.Discount;
            toBeUpdated.Depth = productUpdate.Depth;
            toBeUpdated.Description = productUpdate.Description;
            toBeUpdated.Height = productUpdate.Height;
            toBeUpdated.Image = productUpdate.Image;
            toBeUpdated.ProductName = productUpdate.ProductName;
            toBeUpdated.Width = productUpdate.Width;

            await _catalogContext.SaveChangesAsync();
            return toBeUpdated.ToModel();
        }

    }
}
