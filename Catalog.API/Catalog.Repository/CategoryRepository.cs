using Catalog.Models;
using Catalog.Repository.Entities;
using Catalog.Repository.Sorters;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Catalog.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogdbContext _catalogdbContext;

        public CategoryRepository(CatalogdbContext catalogContext)
        {
            _catalogdbContext = catalogContext;
        }

        public async Task<string> NextId()
        {
            var nextId = await _catalogdbContext.Categories.MaxAsync(c => c.CategoryId);

            if (string.IsNullOrEmpty(nextId)) return "CAT00001";

            nextId = "CAT" + (int.Parse(Regex.Match(nextId, @"\d+").Value) + 1).ToString().PadLeft(5, '0');
            return nextId;
        }

        public async Task<CategoryModel> Add(CategoryAddUpdateModel categoryAdd)
        {
            var existingCategoryWithName = await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryAdd.CategoryName);
            if (existingCategoryWithName == null)
            {
                string nextId = await NextId();
                var newCategory = categoryAdd.ToEntity(nextId);
                await _catalogdbContext.AddAsync(newCategory);
                await _catalogdbContext.SaveChangesAsync();
                return newCategory.ToModel();
            }
            else
            {
                return new CategoryModel
                {
                    CategoryId = "INVALIDALREADYANOTHEREXISTS" + existingCategoryWithName.CategoryId,
                    CategoryName = existingCategoryWithName.CategoryName,
                    CategoryDescription = existingCategoryWithName.CategoryDescription ?? "",
                };
            }


        }

        public async Task<CategoryModel?> Delete(string id)
        {
            var toBeDeleted = await GetEntity(id);
            if (toBeDeleted == null) return null;
            _catalogdbContext.Categories.Remove(toBeDeleted);
            await _catalogdbContext.SaveChangesAsync();
            return toBeDeleted.ToModel();
        }

        private async Task<Category?> GetEntity(string id)
        {
            var FoundCategory = await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (FoundCategory == null) return null;
            return (FoundCategory);
        }

        public async Task<List<CategoryModel>> Get()
        {
            var categories = (await this._catalogdbContext.Categories.ToListAsync());
            var categoriesModel = new List<CategoryModel>();
            categories.ForEach(c => categoriesModel.Add(c.ToModel()));
            return categoriesModel.OrderBy(c => c.CategoryName).ToList();
        }

        public async Task<PagedListModel<CategoryModel>> Get(PaginationationModel paginationationModel)
        {
            var categories = (await this._catalogdbContext.Categories.CustomSortIntoPages(paginationationModel, out PageModel pageModel).ToListAsync());
            var categoriesModel = new List<CategoryModel>();
            categories.ForEach(c => categoriesModel.Add(c.ToModel()));
            return new PagedListModel<CategoryModel>(categoriesModel, pageModel);
        }

        public async Task<CategoryModel?> Get(string id)
        {
            var FoundCategory = await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            FoundCategory ??= await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == id);
            if (FoundCategory == null) return null;
            return FoundCategory.ToModel();
        }

        public async Task<PagedListModel<CategoryModel>> Search(string searchKey, PaginationationModel paginationationModel)
        {
            var categories = await _catalogdbContext.Categories
                .Where(c => c.CategoryName.ToLower().Contains(searchKey) || c.CategoryId.ToLower().Contains(searchKey))
                .CustomSortIntoPages(paginationationModel, out PageModel pageModel)
                .Select(c => c.ToModel())
                .ToListAsync();
            return new PagedListModel<CategoryModel>(categories, pageModel);
        }

        public async Task<CategoryModel?> Update(string id, CategoryAddUpdateModel categoryUpdate)
        {
            var toBeUpdated = await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (toBeUpdated == null) return null;

            var existingCategoryWithName = await _catalogdbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryUpdate.CategoryName);
            if (existingCategoryWithName == null || existingCategoryWithName.CategoryId == toBeUpdated.CategoryId)
            {
                toBeUpdated.CategoryDescription = categoryUpdate.CategoryDescription;
                toBeUpdated.CategoryName = categoryUpdate.CategoryName;
                await _catalogdbContext.SaveChangesAsync();
            }
            else
            {
                return new CategoryModel
                {
                    CategoryId = "INVALIDALREADYANOTHEREXISTS" + existingCategoryWithName.CategoryId,
                    CategoryName = existingCategoryWithName.CategoryName,
                    CategoryDescription = existingCategoryWithName.CategoryDescription ?? "",
                };
            }
            return toBeUpdated.ToModel();
        }

    }
}
