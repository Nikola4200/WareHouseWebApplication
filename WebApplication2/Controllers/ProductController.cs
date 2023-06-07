using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WarehouseWeb.Configuration;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.ClassificationValueDTO;
using WarehouseWeb.Contracts.Common;
using WarehouseWeb.Contracts.ProductDto;
using WarehouseWeb.Contracts.ProductDTO;
using WarehouseWeb.Helpers;
using WarehouseWeb.Model;
using WarehouseWeb.Services;

namespace WarehouseWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {   //Product service ubaciti u service u startapu i da li je scoped?
            _productService = productService;
        }

        [HttpGet]
        [Route("api/controller/GetAllProducts")]
        public async Task<ActionResult<Result<IEnumerable<Product>>>> GetAllProducts(string columnName, string inputReq, string orderRule, int page, int pageSize, [FromQuery] string listOfIds)
        {
            long[] ids = ArrayHelper.ConvertToArrayOfIds(listOfIds);

            InputProductDto inputProductDto = new InputProductDto()
            {
                ListOfProductCategories = ids,
                Page = page,
                PageSize = pageSize,
                SearchFilter = inputReq,
                OrderByColumn = columnName,
                OrderRule = orderRule
            };

            Result result = await _productService.GetAllProducts(inputProductDto);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetProductById")]
        public async Task<ActionResult<Result<Product>>> GetProductById(long id)
        {
                Result result = await _productService.GetProductById(id);
                return GetResultByStatusCode(result);
        }

        [HttpPost]
        [Route("api/controller/AddProduct")]
        public async Task<ActionResult<Result<Product>>> AddProduct(ProductDto productDto)
        {
            Result result = await _productService.CreateProduct(productDto);
            return GetResultByStatusCode(result);

        }

        [HttpDelete]
        [Route("api/controller/DeleteProduct")]
        public async Task<ActionResult<Result<Product>>> DeleteProduct(long id)
        {
            Result result = await _productService.DeleteProduct(id);
            return GetResultByStatusCode(result);
        }

        [HttpPut]
        [Route("api/controller/UpdateProduct")]

        public async Task<ActionResult<Result<Product>>> UpdateProduct(ProductDto productDto)
        {
            Result result = await _productService.UpdateProduct(productDto);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetAllProductsCategories")]
        public async Task<ActionResult<Result<IEnumerable<Product>>>> GetAllProductsCategories()
        {
            Result result = await _productService.GetAllProductsCategories();
            return GetResultByStatusCode(result);
        }
    }
}
