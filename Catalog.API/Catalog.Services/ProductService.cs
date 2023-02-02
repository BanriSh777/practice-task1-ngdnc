using Catalog.Models;
using Catalog.Repository;

namespace Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        
        public ProductService(IProductRepository productRepository)
        {
            _repository= productRepository;
        }
        
        public async Task<ProductSummaryModel?> Add(ProductAddUpdateModel productAdd)
        {
            return await _repository.Add(productAdd);
        }

        public async Task<ProductModel?> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        public async Task<PagedListModel<ProductSummaryModel>> Get(PaginationationModel paginationationModel)
        {
            return await _repository.Get(paginationationModel);
        }

        public async Task<PagedListModel<ProductModel>?> Get(string categoryId, PaginationationModel paginationationModel)
        {
            return await _repository.Get(categoryId, paginationationModel);
        }

        public async Task<ProductModel?> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<PagedListModel<ProductSummaryModel>> Search(string searchKey, PaginationationModel paginationationModel)
        {
            return await _repository.Search(searchKey, paginationationModel);
        }

        public async Task<ProductModel?> Update(string id, ProductAddUpdateModel productUpdate)
        {
            return await _repository.Update(id, productUpdate);
        }
    }
}
