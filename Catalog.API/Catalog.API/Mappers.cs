using Catalog.Models;
using System.Text;

namespace Catalog.API
{
    public static class Mappers
    {
        public static CategoryAddUpdateModel ToModel(this CategoryAddUpdateDTO categoryAddDTO)
        {
            return new CategoryAddUpdateModel(categoryAddDTO.CategoryName, categoryAddDTO.CategoryDescription);
        }

        public static ProductAddUpdateModel ToModel(this ProductAddUpdateDTO dto)
        {
            decimal price = (dto.Mrp != 0 && (dto.Price == 0 || (dto.Price > dto.Mrp && dto.Discount != 0 && dto.Discount !> dto.Mrp))) ? dto.Mrp - dto.Discount : dto.Price;
            decimal mrp = (dto.Mrp < price) ? price + dto.Discount : dto.Mrp;
            decimal discount = (dto.Discount != mrp - price) ? mrp - price : dto.Discount;
            string b64 = dto.Image?.Replace("data:image/png;base64,", String.Empty) ?? String.Empty;

            return new ProductAddUpdateModel(dto.ProductName, dto.CategoryId, price)
            {
                Description = dto.Description,
                Discount = discount,
                Mrp = mrp,
                Width = dto.Width,
                Height = dto.Height,
                Depth = dto.Depth,
                Image = Convert.FromBase64String(b64),
            };
        }

        public static PaginationationModel ToModel(this PaginationDTO paginationDTO)
        {
            return new PaginationationModel(paginationDTO.PageNumber, paginationDTO.PageSize, paginationDTO.SortBy)
            {
                IsDesc = paginationDTO.IsDesc,
            };
        }

        public static PagedListDTO<CategoryDTO> ToDTO(this PagedListModel<CategoryModel> pagedListModel)
        {
            return new PagedListDTO<CategoryDTO>(pagedListModel.Data.Select(c => c.ToDTO()).ToList(), pagedListModel.Page.ToDTO());
        }

        public static PagedListDTO<ProductSummaryDTO> ToDTO(this PagedListModel<ProductSummaryModel> pagedListModel)
        {
            return new PagedListDTO<ProductSummaryDTO>(pagedListModel.Data.Select(p => p.ToDTO()).ToList(), pagedListModel.Page.ToDTO());
        }
        public static PagedListDTO<ProductDTO> ToDTO(this PagedListModel<ProductModel> pagedListModel)
        {
            return new PagedListDTO<ProductDTO>(pagedListModel.Data.Select(p => p.ToDTO()).ToList(), pagedListModel.Page.ToDTO());
        }

        public static PageDTO ToDTO(this PageModel pageModel)
        {
            return new PageDTO(pageModel.PageNumber,
                    pageModel.PageSize,
                    pageModel.TotalPages,
                    pageModel.RecordsInPage,
                    pageModel.TotalRecords,
                    pageModel.SortedBy,
                    pageModel.IsDesc
                );
        }

        public static CategoryDTO ToDTO(this CategoryModel categoryModel)
        {
            return new CategoryDTO
            (
                 categoryModel.CategoryId,
                categoryModel.CategoryName,
                categoryModel.CategoryDescription
            );
        }

        public static ProductDTO ToDTO(this ProductModel productModel)
        {
            return new ProductDTO(
                productModel.ProductId,
                productModel.ProductName,
                productModel.CategoryName,
                productModel.CategoryId,
                productModel.Description,
                productModel.Price,
                productModel.Mrp,
                productModel.Discount,
                productModel.Height,
                productModel.Depth,
                productModel.Width,
                Encoding.UTF8.GetString(productModel.Image)
               ) ;
        }

        public static ProductSummaryDTO ToDTO(this ProductSummaryModel productSummaryModel)
        {
            return new ProductSummaryDTO(
                productSummaryModel.ProductId,
                productSummaryModel.ProductName,
                productSummaryModel.Price,
                productSummaryModel.CategoryId,
                productSummaryModel.CategoryName,
                productSummaryModel.Description
            );
        }

    }
}
