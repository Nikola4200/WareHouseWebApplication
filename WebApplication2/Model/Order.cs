using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Data.Enum;

namespace WarehouseWeb.Model
{
    public class Order : CoreObject
    {
        public DateTime? DeliveryDate { get; set; }

        public string? City { get; set; }

        public string? Adress { get; set; }

        public Status Status { get; set; }

        public double? Price { get; private set; }

        public List<OrderItem> OrderItemList { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public void SetTotalPriceForOrder()
        {
            Price = OrderItemList.Sum(x => x.Price * x.QuantityAmount.QuantityAmount);
        }

    }
}
