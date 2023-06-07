using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.StorageItemDTO
{
    public class StorageItemDto
    {
        public long Id { get; set; }
        public Quantity QuantityAmount { get; set; }

       // public string UnitOfMeasurement { get; set; }

        public long ProductId { get; set; }

        public long StorageId { get; set; }
    }
}
