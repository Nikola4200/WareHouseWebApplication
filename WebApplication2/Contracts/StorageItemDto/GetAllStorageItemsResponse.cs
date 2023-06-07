using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.StorageItemDto
{
    public class GetAllStorageItemsResponse
    {
        public GetAllStorageItemsResponse(long id, Quantity quantityAmount, long productId, long storageId)
        {
            this.Id = id;
            this.QuantityAmount = quantityAmount;
            this.ProductId = productId;
            this.StorageId = storageId;
        }

        public long Id { get; set; }
        public Quantity QuantityAmount { get; set; }

        public long ProductId { get; set; }

        public long StorageId { get; set; }

    }
}
