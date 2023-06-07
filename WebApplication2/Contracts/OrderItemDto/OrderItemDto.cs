using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.OrderItemDTO
{
    public class OrderItemDto
    {
        public long Id { get; set; }

        public Quantity QuantityAmount { get; set; }

        
        public double Price { get; set; }

       // public string UnitOfMeasurement { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

       // public double Price { get; set; }
    }
}
