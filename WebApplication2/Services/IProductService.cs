using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.ProductDto;
using WarehouseWeb.Contracts.ProductDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Model.Repository;

namespace WarehouseWeb.Services
{
    public interface IProductService
    {
        Task<Result> CreateProduct(ProductDto productDto);

        Task<Result> UpdateProduct(ProductDto productDto);

        Task<Result> DeleteProduct(long productId);

        Task<Result> GetAllProducts(InputProductDto inputProductDto);
        //IEnumerable<Result>

        Task<Result> GetProductById(long productId);

        Task<Result> GetAllProductsCategories();

        
    }
}
