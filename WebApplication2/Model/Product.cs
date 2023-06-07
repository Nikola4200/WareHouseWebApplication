using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class Product : CoreObject
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public Supplier Supplier { get; set; }

        public long SupplierId { get; set; }

        public ClassificationValue ClassificationValue { get; set; }

        public long ClassificationValueId { get; set; }
    }
}
