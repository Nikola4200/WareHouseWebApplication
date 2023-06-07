using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class Role : CoreObject
    { 
        public string Name { get; set; }

        public List<UserRole> UserRoleList { get; set; }
        public List<RoleClaims> ActivitiesRoleList { get; set; }
    }
}
