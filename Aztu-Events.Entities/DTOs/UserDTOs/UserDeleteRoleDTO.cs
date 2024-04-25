using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.UserDTOs
{
    public class UserDeleteRoleDTO
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }

    }
}
