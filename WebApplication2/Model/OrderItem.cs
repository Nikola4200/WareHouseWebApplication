using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class OrderItem : CoreObject
    {
        public Quantity QuantityAmount { get; set; }

        [NotMapped]
        public double Price { get; set; }

        //public string UnitOfMeasurement { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        public long ProductId { get; set; }

    

    }
}
