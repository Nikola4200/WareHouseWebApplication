using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.ProductDTO
{
    public class ProductDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public long SupplierId { get; set; }

        public long ClassificationValueId { get; set; }

        //public Quantity QuantityAmount { get; set; }
    }
}
