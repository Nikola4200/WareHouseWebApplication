using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.SupplierDTO;
using WarehouseWeb.Model;

namespace WarehouseWeb.Services
{
    public interface ISupplierService
    {
        Task<Result> CreateSupplier(SupplierDto supplierDto);

        Task<Result> UpdateSupplier(SupplierDto supplierDto);

        Task<Result> DeleteSupplier(long id);

        Task<Result> GetSupplierById(long id);

        Task<Result> GetAllSupplier();

        
    }
}
