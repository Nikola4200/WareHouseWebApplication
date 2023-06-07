using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class Storage : CoreObject
    {
        public string? Description { get; set; }

        public string City { get; set; } 

        public List<StorageItem> StorageItemList { get; set; } 
    }
}
