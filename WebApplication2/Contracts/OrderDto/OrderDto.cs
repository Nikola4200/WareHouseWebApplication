using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts.OrderItemDTO;
using WarehouseWeb.Data.Enum;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.OrderDTO
{
    public class OrderDto
    {
        public long Id { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string City { get; set; }

        public Status Status { get; set; }

        public string Adress { get; set; }

        public double Price { get; set; }

        public List<OrderItemDto> OrderItemList { get; set; }

        public long UserId { get; set; }
    }
}
