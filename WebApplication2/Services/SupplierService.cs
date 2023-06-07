using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.SupplierDto;
using WarehouseWeb.Contracts.SupplierDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Model.Repository;

namespace WarehouseWeb.Services
{
    public class SupplierService : ISupplierService
    {

        private readonly IGenericRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IGenericRepository<Supplier> supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateSupplier(SupplierDto supplierDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            //Da li moze da se napravi bezparametarski ctor i onda da se definise samo Result result = new Result(); i onda kasnije da mu se dodele prop...
            if (supplierDto == null)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Can't insert null object";
                return result;
            }

            Supplier supplier = new Supplier();
            supplier.Name = supplierDto.Name;
            supplier.Adress = supplierDto.Adress;
            supplier.City = supplierDto.City;

            bool isAdded = await _supplierRepository.Add(supplier);

            if (isAdded == false)
            {
                result.Value = isAdded;
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Couldn't add supplier to database";
                return result;
            }

            _unitOfWork.commit();
            result.Value = isAdded;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> DeleteSupplier(long id)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (id == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Supplier ID must be greater than 0";
                return result;
            }

            Supplier supplier = _supplierRepository.GetQueryable<Supplier>()
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (supplier == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Supplier can't be found, please enter correct ID";
                return result;
            }

            bool isDeleted = await _supplierRepository.Delete(supplier);

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

        public async Task<Result> GetAllSupplier()
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            //List<Supplier> suppliers = (List<Supplier>) await _supplierRepository.GetAll();

            List<GetAllSuppliersResponse> suppliersResponseList = _supplierRepository.GetQueryable<Supplier>()
                .Select(x => new GetAllSuppliersResponse(x.Id, x.Name, x.City, x.Adress))
                .AsNoTracking()
                .ToList();

            if (suppliersResponseList == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "List of suppliers is empty";
                return result;
            }

            result.Value = suppliersResponseList;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetSupplierById(long id)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (id == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Supplier ID must be greater than 0";
                return result;
            }

            //Supplier supplier = await _supplierRepository.GetById(id);

            GetSupplierByIdResponse supplierResponse = _supplierRepository.GetQueryable<Supplier>()
                .Where(x => x.Id == id)
                .Select(x => new GetSupplierByIdResponse(x.Id, x.Name, x.City, x.Adress))
                .AsNoTracking()
                .FirstOrDefault();

            if (supplierResponse == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Supplier can't be found, please enter correct ID";
                return result;
            }
            result.Value = supplierResponse;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> UpdateSupplier(SupplierDto supplierDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (supplierDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to insert null object";
                return result;
            }

            if (supplierDto.Id == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Enter the correct Supplier ID";
                return result;
            }

            Supplier supplier = _supplierRepository.GetQueryable<Supplier>()
                .Where(x => x.Id == supplierDto.Id)
                .FirstOrDefault();

            if (supplier == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Couldnt find supplier with given ID";
                return result;
            }

            supplier.Name = supplierDto.Name;
            supplier.Adress = supplierDto.Adress;
            supplier.City = supplierDto.City;

            bool isUpdated = await _supplierRepository.Update(supplier);

            if (isUpdated == false)
            {
                result.Value = isUpdated;
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Couldn't update Supplier";
                return result;
            }

            _unitOfWork.commit();
            result.Value = isUpdated;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }
    }
}
