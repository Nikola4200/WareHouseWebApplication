using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts.Common;
using WarehouseWeb.Model;
using WarehouseWeb.Contracts.ClassificationValueDTO;

namespace WarehouseWeb.Contracts.ProductDto
{
    public class InputProductDto : PaginatorDto
    {
        public string OrderByColumn { get; set; }
        public string OrderRule { get; set; }
        public string SearchFilter { get; set; }
        public long[]? ListOfProductCategories { get; set; }

    }
}
