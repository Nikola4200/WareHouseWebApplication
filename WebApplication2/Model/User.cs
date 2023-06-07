using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Data.Enum;

namespace WarehouseWeb.Model
{
    
    public class User : CoreObject
    { 

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        public Company Company { get; set; }
        public long CompanyId { get; set; }

        public List<UserRole> UserRoleList { get; set; }

        
        //public long ApplicationUserId { get; set; }

    }

}
