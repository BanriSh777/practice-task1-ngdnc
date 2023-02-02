using Catalog.Models;

namespace Catalog.Services
{
    public interface IProductService
    {
        public Task<PagedListModel<ProductSummaryModel>> Get(PaginationationModel paginationationModel);
        public Task<PagedListModel<ProductModel>?> Get(string categoryId, PaginationationModel paginationationModel);
        public Task<ProductModel?> Get(string id);
        public Task<ProductSummaryModel?> Add(ProductAddUpdateModel productAdd);
        public Task<ProductModel?> Update(string id, ProductAddUpdateModel productUpdate);
        public Task<ProductModel?> Delete(string id);
        public Task<PagedListModel<ProductSummaryModel>> Search(string searchKey, PaginationationModel paginationationModel);
    }
}
