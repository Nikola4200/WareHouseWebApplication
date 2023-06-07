using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Data.Enum;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.StorageItemInputOutputDTO
{
    public class StorageItemInputOutputDto
    {
        public long Id { get; set; }
        public Quantity QuantityAmount { get; set; }
        public ItemActivity ItemActivity { get; set; }
        public long StorageItemId { get; set; }
    }
}
