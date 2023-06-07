using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class StorageItem : CoreObject
    {
        public Quantity QuantityAmount { get; set; }

        public Product Product { get; set; }
        public long ProductId { get; set; }

        public Storage Storage { get; set; }
        public long StorageId { get; set; }

        public List<StorageItemInputOutput> ListOfStorageItemInputOutputs { get; set; }

        internal void SetQuantityAmount()
        {
            double sumOfInputs = ListOfStorageItemInputOutputs.Where(x => x.ItemActivity == Data.Enum.ItemActivity.Input)
                .Sum(x => x.QuantityAmount.QuantityAmount);
            double sumOfOutputs = ListOfStorageItemInputOutputs.Where(d => d.ItemActivity == Data.Enum.ItemActivity.Output)
                                 .Sum(d => d.QuantityAmount.QuantityAmount);
            QuantityAmount.QuantityAmount = sumOfInputs - sumOfOutputs;
        }
    }
}
