using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Contracts.StorageDto
{
    public class GetAllStoragesResponse
    {
        public GetAllStoragesResponse(long id, string description, string city)
        {
            this.Id = id;
            this.Description = description;
            this.City = city;
        }
        public long Id { get; set; }

        public string? Description { get; set; }

        public string City { get; set; }
    }
}
