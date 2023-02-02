using Catalog.Models;
using Catalog.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Repository.Sorters
{
    public static class CustomPageSort
    {
        public static IQueryable<Category> CustomSortIntoPages(this IQueryable<Category> categories, PaginationationModel paginationModel, out PageModel pagesModel)
        {
            var querychain = categories.AsQueryable();
            string sortedBy = "";
            int pageSize = paginationModel.PageSize,
                pageNumber = paginationModel.PageNumber,
                totalRecords = querychain.Count(),
                totalPages = (int) Math.Ceiling(totalRecords / (float)pageSize);

            if (paginationModel.IsDesc)
            {
                switch (paginationModel.SortBy.ToLowerInvariant())
                {
                    case "categoryid":
                        querychain = categories.OrderByDescending(c => c.CategoryId);
                        sortedBy = "categoryid";
                        break;
                    case "categoryname":
                        querychain = categories.OrderByDescending(c => c.CategoryName);
                        sortedBy = "categoryname";
                        break;

                    default:
                        querychain = categories.OrderByDescending(c => c.CategoryId);
                        sortedBy = "categoryid";
                        break;
                }
            } 
            else
            {
                switch (paginationModel.SortBy.ToLowerInvariant())
                {
                    case "categoryid":
                        sortedBy = "categoryid";
                        querychain = categories.OrderBy(c => c.CategoryId);
                        break;

                    case "categoryname":
                        sortedBy = "categoryname";
                        querychain = categories.OrderBy(c => c.CategoryName);
                        break;

                    default:
                        sortedBy = "categoryid";
                        querychain = categories.OrderBy(c => c.CategoryId);
                        break;
                }
            }
            
            querychain = querychain.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            
            pagesModel = new PageModel(sortedBy)
            {
                IsDesc = paginationModel.IsDesc,
                PageNumber = paginationModel.PageNumber,
                PageSize = pageSize,
                RecordsInPage = querychain.Count(),
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };
            return querychain;
        }
        public static IQueryable<Product> CustomSortIntoPages(this IQueryable<Product> products, PaginationationModel paginationModel, out PageModel pageModel)
        {
            var querychain = products.AsQueryable();
            string sortedBy = "";
            int pageSize = paginationModel.PageSize,
                pageNumber = paginationModel.PageNumber,
                totalRecords = querychain.Count(),
                totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            if (paginationModel.IsDesc)
            {
                switch (paginationModel.SortBy.ToLower())
                {
                    case "categoryid":
                        querychain = products.OrderByDescending(p => p.CategoryId);
                        sortedBy = "categoryid";
                        break;

                    case "productid":
                        querychain = products.OrderByDescending(p => p.ProductId);
                        sortedBy = "productid";
                        break;

                    case "productname":
                        querychain = products.OrderByDescending(p => p.ProductName);
                        sortedBy = "productname";
                        break;

                    case "price":
                        querychain = products.OrderByDescending(p => p.Price);
                        sortedBy = "price";
                        break;

                    case "categoryname":
                        querychain = products.OrderByDescending(p => p.Category!.CategoryName);
                        sortedBy = "categoryname";
                        break;

                    default:
                        querychain = products.OrderByDescending(c => c.ProductId);
                        sortedBy = "productid";
                        break;
                }
            }
            else
            {
                switch (paginationModel.SortBy.ToLowerInvariant())
                {
                    case "categoryid":
                        querychain = products.OrderBy(p => p.CategoryId);
                        sortedBy = "categoryid";
                        break;

                    case "productid":
                        querychain = products.OrderBy(p => p.ProductId);
                        sortedBy = "productid";
                        break;

                    case "productname":
                        querychain = products.OrderBy(p => p.ProductName);
                        sortedBy = "productname";
                        break;

                    case "price":
                        querychain = products.OrderBy(p => p.Price);
                        sortedBy = "price";
                        break;

                    case "categoryname":
                        querychain = products.OrderBy(p => p.Category!.CategoryName);
                        sortedBy = "categoryname";
                        break;

                    default:
                        querychain = products.OrderBy(c => c.ProductId);
                        sortedBy = "productid";
                        break;
                }
            }
            
            querychain = querychain.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            pageModel = new PageModel(sortedBy)
            {
                IsDesc = paginationModel.IsDesc,
                PageNumber = paginationModel.PageNumber,
                PageSize = pageSize,
                RecordsInPage = querychain.Count(),
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };
            return querychain;
        }
    }
}
