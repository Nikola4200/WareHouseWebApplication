using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class Company : CoreObject
    {
        public string Name { get; set; }

        public string City { get; set; }
    }
}
