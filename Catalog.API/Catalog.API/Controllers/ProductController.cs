using Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListDTO<ProductSummaryDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            paginationDTO = paginationDTO.CheckPagination();
            return Ok((await _productService.Get(paginationDTO.ToModel())).ToDTO());
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(string id)
        {
            var product = (await _productService.Get(id))?.ToDTO();
            if (product == null) return NotFound(new
            {
                message = $"Product with id :{id} is not found"
            });
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductSummaryDTO>?> Add(ProductAddUpdateDTO productAdd)
        {
            if(string.IsNullOrWhiteSpace(productAdd.ProductName)) return BadRequest(new {message =  "Enter a valid Name of the product" });
            var newProduct = await _productService.Add(productAdd.ToModel());
            if (newProduct == null) return NotFound(new
            {
                message = $"Category with Id {productAdd.CategoryId} doesn't exit"
            });

            return CreatedAtRoute("GetProduct", new
            {
                id = newProduct.ProductId
            }, newProduct);
        }

        [HttpPost("category/{categoryId}")]
        public async Task<ActionResult<PagedListDTO<ProductDTO>>> Get(string categoryId, PaginationDTO paginationDTO)
        {
            paginationDTO = paginationDTO.CheckPagination();
            var productsPages = (await _productService.Get(categoryId, paginationDTO.ToModel()))?.ToDTO();
            if (productsPages == null) return NotFound(new { message = $"Category with id {categoryId} is not found" });
            return Ok(productsPages);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PagedListDTO<ProductSummaryDTO>>> Search([FromQuery] string key, PaginationDTO paginationDTO)
        {
            paginationDTO = paginationDTO.CheckPagination();
            return (await _productService.Search(key, paginationDTO.ToModel())).ToDTO();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, ProductAddUpdateDTO productUpdate)
        {
            if (string.IsNullOrWhiteSpace(productUpdate.ProductName) ||
                string.IsNullOrWhiteSpace(productUpdate.CategoryId)) return BadRequest(new { message = "Enter a valid Name of the product" });
            var updated = await _productService.Update(id, productUpdate.ToModel());
            if (updated == null) return NotFound(new { message = $"Product with Id {id} not found" });
            if (updated.CategoryId.Contains("INVALIDCATEGORY")) return NotFound(new { message = $"Category with Id {productUpdate.CategoryId} not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _productService.Delete(id);
            if (deleted == null) return NotFound(new { message = $"Product with Id {id} not found" });
            return NoContent();
        }
    }
}
