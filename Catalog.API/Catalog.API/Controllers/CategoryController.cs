using Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<PagedListDTO<CategoryDTO>>> Get([FromQuery]PaginationDTO paginationDTO)
        {
            paginationDTO = paginationDTO.CheckPagination();
            var categoriesPages = await _categoryService.Get(paginationDTO.ToModel());
            return Ok(categoriesPages.ToDTO());
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(string id)
        {
            var category = await _categoryService.Get(id);
            if(category== null)
            {
                return NotFound(new
                {
                    message = $"Category with Id {id} not found"
                });
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Add(CategoryAddUpdateDTO categoryAdd)
        {
            if (string.IsNullOrWhiteSpace(categoryAdd.CategoryName))
            {
                return BadRequest(new {
                    message = "Enter a valid Name for Category"
                });
            }
            var newCategory = await _categoryService.Add(categoryAdd.ToModel());
            if (newCategory.CategoryId.Contains("INVALIDALREADYANOTHEREXISTS"))
            {
                newCategory.CategoryId = newCategory.CategoryId.Replace("INVALIDALREADYANOTHEREXISTS", "");
                return Conflict(new 
                {
                    message = "Category with Name : " + categoryAdd.CategoryName + " already exists!! ",
                    conflict = new {
                        newCategory.CategoryId,
                        newCategory.CategoryName,
                        newCategory.CategoryDescription,
                    }
                });
            }
            return CreatedAtRoute("GetCategory", new
            {
                id = newCategory.CategoryId,
            }, newCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _categoryService.Delete(id);
            if(deleted == null) return  NotFound(new
            {
                message = $"Category with Id {id} not found"
            });
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, CategoryAddUpdateDTO updateDTO)
        {
            var updated = await _categoryService.Update(id, updateDTO.ToModel());
            if(updated == null) return NotFound();
            if (updated.CategoryId.Contains("INVALIDALREADYANOTHEREXISTS"))
            {
                updated.CategoryId = updated.CategoryId.Replace("INVALIDALREADYANOTHEREXISTS", "");
                updated.CategoryId = updated.CategoryId.Replace("INVALIDALREADYANOTHEREXISTS", "");
                return Conflict(new
                {
                    message = "Category with Name : " + updated.CategoryName + " already exists!! ",
                    conflict = new
                    {
                        updated.CategoryId,
                        updated.CategoryName,
                        updated.CategoryDescription,
                    }
                });
            }
            return Ok(updated);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PagedListDTO<CategoryDTO>>> Search([FromQuery] string key, PaginationDTO paginationDTO)
        {
            paginationDTO = paginationDTO.CheckPagination();
            var categoriesPages = await _categoryService.Search(key.ToLowerInvariant(), paginationDTO.ToModel());
            return Ok(categoriesPages.ToDTO());
        }
    }

    static class Utility
    {
        public static PaginationDTO CheckPagination(this PaginationDTO paginationDTO)
        {
            int pageNumber = paginationDTO.PageNumber,
                pageSize = paginationDTO.PageSize;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;
            if (pageNumber <= 0) pageNumber = 1;
            return new PaginationDTO(pageNumber, pageSize, paginationDTO.SortBy, paginationDTO.IsDesc);
        }
    }
}
