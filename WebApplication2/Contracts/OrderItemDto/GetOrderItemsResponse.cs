using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.OrderItemDTO
{
    public class GetOrderItemsResponse
    {
        public GetOrderItemsResponse(long id, Quantity quantityAmount, long orderId, long productId)
        {
            this.Id = id;
            this.QuantityAmount = quantityAmount;
            this.OrderId = orderId;
            this.ProductId = productId;
            //this.Price = price;
        }
        public long Id { get; set; }

        public Quantity QuantityAmount { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        //public double Price { get; set; }
    }
}
