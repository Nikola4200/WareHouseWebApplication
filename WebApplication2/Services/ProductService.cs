using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WarehouseWeb.Configuration;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.ClassificationValueDTO;
using WarehouseWeb.Contracts.ProductDto;
using WarehouseWeb.Contracts.ProductDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Model.Repository;


namespace WarehouseWeb.Services
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ClassificationValue> _classificationValueRepository;

        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> productRepository, IGenericRepository<ClassificationValue> classificationValueRepository)
        {
            _unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this._classificationValueRepository = classificationValueRepository;
        }

        public async Task<Result> CreateProduct(ProductDto productDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (productDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to insert null object";
                return result;
            }

            Product product = new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.SupplierId = productDto.SupplierId;
            product.ClassificationValueId = productDto.ClassificationValueId;

            bool isAdded = await productRepository.Add(product);

            if (isAdded == false)
            {
                result.Value = isAdded;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been added";
                return result;
            }

            _unitOfWork.commit();
            result.StatusCode = StatusCodes.Status200OK;
            result.Value = isAdded;
            return result;
        }

        public async Task<Result> DeleteProduct(long productId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            if (productId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Product ID must be greater than 0";
                return result;
            }

            Product product = productRepository.GetQueryable<Product>()
           .Where(x => x.Id == productId)
           .AsNoTracking()
           .FirstOrDefault();

            if (product == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Product can't be found, please enter correct ID";
                return result;
            }

            bool isDeleted = await productRepository.Delete(product);

            if (isDeleted == false)
            {
                result.Value = isDeleted;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been deleted";
                return result;
            }
            _unitOfWork.commit();
            result.Value = isDeleted;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetAllProducts(InputProductDto inputProductDto)
        {
            int totalCount = 0;
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            var prFilter = PredicateBuilder.True<Product>();

            if (!string.IsNullOrWhiteSpace(inputProductDto.SearchFilter))
            {
                prFilter = prFilter.And(x => x.Name.Contains(inputProductDto.SearchFilter));
            }

            if(inputProductDto.ListOfProductCategories.Any())
            {
                prFilter = prFilter.And(x => inputProductDto.ListOfProductCategories.Any(y => y == x.ClassificationValueId));
            }

            IQueryable<Product> listProducts = productRepository.GetQueryable<Product>()
            .AsNoTracking()
            .Where(prFilter);
                //.Where(y => inputProductDto.ListOfProductCategories.Any(x => x == y.ClassificationValueId));
            //Preko predicate buildera uraditi ovaj where uslov
            


            listProducts = listProducts.OrderByString(inputProductDto.OrderByColumn, inputProductDto.OrderRule);

            List<GetAllProductsResponse> listOfProductsResponse = listProducts
                //skinuta paginacija
                .CountOut(out totalCount)
                .Skip((inputProductDto.Page - 1) * inputProductDto.PageSize)
                .Take(inputProductDto.PageSize)
                .Select(x => new GetAllProductsResponse(x.Id, x.Name, x.Description, x.Price))
                .ToList();


            if (listOfProductsResponse == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Products can't be found";
                return result;
            }
            result.TotalCount = totalCount;
            result.StatusCode = StatusCodes.Status200OK;
            result.Value = listOfProductsResponse;
            return result;
        }

        public async Task<Result> GetProductById(long productId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (productId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Product ID must be greater than 0";
                return result;
            }

            GetProductByIdResponse productResponse = productRepository.GetQueryable<Product>()
                .Where(x => x.Id == productId)
                .Select(x => new GetProductByIdResponse(x.Id, x.Name, x.Description, x.Price))
                .AsNoTracking()
                .FirstOrDefault();

            if (productResponse == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Product can't be found";
                return result;
            }
            result.Value = productResponse;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> UpdateProduct(ProductDto productDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (productDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to update null object";
                return result;
            }

            if (productDto.Id == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Must insert ID of existing product";
                return result;
            }

            Product product =  productRepository.GetQueryable<Product>()
                .Where(x => x.Id == productDto.Id)
                .FirstOrDefault();

            if (product == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Couldn't find product";
                return result;
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;

            bool isUpdated = await productRepository.Update(product);

            if (isUpdated == false)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Couldn't update product";
                return result;
            }

            _unitOfWork.commit();
            result.StatusCode = StatusCodes.Status200OK;
            result.Value = isUpdated;
            return result;
        }

        public async Task<Result> GetAllProductsCategories()
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            List<GetAllClassificationValuesResponse> listOfCategories = _classificationValueRepository.GetQueryable<ClassificationValue>()
                .AsNoTracking()
                .Where(x=> x.ClassificationSpecification.Name == "ProductsCategory")
                .Select(x => new GetAllClassificationValuesResponse(x.Id, x.Name))
                .ToList();

            if(listOfCategories == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Not found categories";
                return result;
            }

            result.Value = listOfCategories;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

    }
}
