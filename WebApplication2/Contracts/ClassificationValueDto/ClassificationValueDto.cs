using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.ClassificationValueDTO
{
    public class ClassificationValueDto
    {

        public ClassificationValueDto(long id)
        {
            this.Id = id;
        }
        public long Id { get; set; }
    }
}
