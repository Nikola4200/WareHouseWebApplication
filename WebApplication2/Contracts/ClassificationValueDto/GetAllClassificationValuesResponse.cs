using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.ClassificationValueDTO
{
    public class GetAllClassificationValuesResponse
    {
        public GetAllClassificationValuesResponse(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
