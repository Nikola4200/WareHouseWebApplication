using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.SupplierDTO
{
    public class SupplierDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }
    }
}
