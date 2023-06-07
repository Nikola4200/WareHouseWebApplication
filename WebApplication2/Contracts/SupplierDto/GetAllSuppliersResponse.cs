using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.SupplierDto
{
    public class GetAllSuppliersResponse
    {

        public GetAllSuppliersResponse(long id, string name, string city, string adress)
        {
            this.Id = id;
            this.Name = name;
            this.City = city;
            this.Adress = adress;
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }
    }
}
