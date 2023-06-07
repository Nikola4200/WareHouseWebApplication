using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.StorageDTO
{
    public class StorageDto
    {
        public long Id { get; set; }

        public string? Description { get; set; } 

        public string City { get; set; }

        //public List<StorageItemDto> StorageItemList { get; set; }
    }
}
