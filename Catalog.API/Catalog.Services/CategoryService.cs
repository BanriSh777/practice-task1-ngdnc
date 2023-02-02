using Catalog.Models;
using Catalog.Repository;

namespace Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<CategoryModel> Add(CategoryAddUpdateModel categoryAdd)
        {
            return _categoryRepository.Add(categoryAdd);
        }

        public Task<CategoryModel?> Delete(string id)
        {
            return (_categoryRepository.Delete(id));
        }

        public Task<List<CategoryModel>> Get()
        {
            return _categoryRepository.Get();
        }

        public Task<PagedListModel<CategoryModel>> Get(PaginationationModel paginationationModel)
        {
            return _categoryRepository.Get(paginationationModel);
        }

        public Task<CategoryModel?> Get(string id)
        {
            return _categoryRepository.Get(id);
        }

        public Task<PagedListModel<CategoryModel>> Search(string searchKey, PaginationationModel paginationationModel)
        {
            return _categoryRepository.Search(searchKey, paginationationModel);
        }

        public Task<CategoryModel?> Update(string id, CategoryAddUpdateModel categoryUpdate)
        {
            return _categoryRepository.Update(id, categoryUpdate);
        }
    }
}
