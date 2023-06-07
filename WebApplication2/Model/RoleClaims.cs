using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class RoleClaims : CoreObject
    {
        public Role Role { get; set; }

        public long RoleId { get; set; }

        public Claims Activities { get; set; }

        public long ActivitiesId { get; set; }
    }
}
