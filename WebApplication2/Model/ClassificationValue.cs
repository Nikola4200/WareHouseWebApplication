using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class ClassificationValue : CoreObject
    {
        public string Name { get; set; }

        public long ClassificationSpecificationId { get; set; }

        public ClassificationSpecification ClassificationSpecification { get; set; }

        


    }
}
