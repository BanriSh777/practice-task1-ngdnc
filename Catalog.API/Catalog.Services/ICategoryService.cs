using Catalog.Models;

namespace Catalog.Services
{
    public interface ICategoryService
    {
        public Task<List<CategoryModel>> Get(); 
        public Task<PagedListModel<CategoryModel>> Get(PaginationationModel paginationationModel); 
        public Task<CategoryModel?> Get(string id); 
        public Task<CategoryModel> Add(CategoryAddUpdateModel categoryAdd); 
        public Task<CategoryModel?> Update(string id, CategoryAddUpdateModel categoryUpdate); 
        public Task<CategoryModel?> Delete(string id); 
        public Task<PagedListModel<CategoryModel>> Search(string searchKey, PaginationationModel paginationationModel); 
    }
}
