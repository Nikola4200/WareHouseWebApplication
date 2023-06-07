using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Data.Enum;
using WarehouseWeb.Model;

namespace WarehouseWeb.Contracts.UserDTO
{
    public class UserDto
    {
       // public string Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        //public Company Company { get; set; }
       // public long CompanyId { get; set; }

        //public List<UserRole> UserRoleList { get; set; }
    }
}
