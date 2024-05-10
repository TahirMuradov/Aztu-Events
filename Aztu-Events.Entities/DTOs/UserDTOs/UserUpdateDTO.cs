using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string UserName { get; set; }


        public string Lastname { get; set; }


       
        public string PhoneNumber { get; set; }

    
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    
        public string? NewPassword { get; set; }


        public string? NewConfirmPassword { get; set; }

    }
}
