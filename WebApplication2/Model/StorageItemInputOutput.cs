using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Data.Enum;

namespace WarehouseWeb.Model
{ 
    public class StorageItemInputOutput : CoreObject
    {
        public Quantity QuantityAmount { get; set; }
        public ItemActivity ItemActivity { get; set; }
        public long StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }

        internal void getSumOfOutputs()
        {
            throw new NotImplementedException();
        }
    }
}
