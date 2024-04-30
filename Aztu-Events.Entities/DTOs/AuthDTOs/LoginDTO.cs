using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AuthDTOs
{
    public class LoginDTO
    {
       
        public string Email { get; set; }


        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
