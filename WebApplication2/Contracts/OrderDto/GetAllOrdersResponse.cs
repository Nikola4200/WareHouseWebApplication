using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.OrderDto
{
    public class GetAllOrdersResponse
    {
        public GetAllOrdersResponse(long id, DateTime? deliveryDate, string city, string adress, double? price)
        {
            this.Id = id;
            this.DeliveryDate = deliveryDate;
            this.City = city;
            this.Adress = adress;
            this.Price = price;
        }

        public long Id { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string? City { get; set; }

        public string? Adress { get; set; }

        public double? Price { get; set; }
    }
}
