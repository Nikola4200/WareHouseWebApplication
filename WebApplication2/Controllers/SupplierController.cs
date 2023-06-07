using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.SupplierDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Services;

namespace WarehouseWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        [Route("api/controller/AddSupplier")]
        public async Task<ActionResult<Result<Supplier>>> AddSupplier(SupplierDto supplierDto)
        {
            Result result = await _supplierService.CreateSupplier(supplierDto);
            return GetResultByStatusCode(result);
        }

        [HttpDelete]
        [Route("api/controller/DeleteSupplier")]
        public async Task<ActionResult<Result<Supplier>>> DeleteSupplier(long id)
        {
            Result result = await _supplierService.DeleteSupplier(id);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetProductById")]
        public async Task<ActionResult<Result<Supplier>>> GetProductById(long id)
        {
            Result result = await _supplierService.GetSupplierById(id);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetAllSupplier")]
        public async Task<ActionResult<Result<IEnumerable<Supplier>>>> GetAllSupplier()
        {
            Result result = await _supplierService.GetAllSupplier();
            return GetResultByStatusCode(result);
        }

        [HttpPut]
        [Route("api/controller/UpdateSupplier")]
        public async Task<ActionResult<Result<Supplier>>> UpdateSupplier(SupplierDto supplierDto)
        {
            Result result = await _supplierService.UpdateSupplier(supplierDto);
            return GetResultByStatusCode(result);
        }
    }
}
