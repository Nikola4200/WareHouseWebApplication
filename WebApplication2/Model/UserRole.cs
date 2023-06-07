using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class UserRole : CoreObject
    { 
        public Role Role { get; set; }
        public long RoleId { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

    }
}
