﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
       

        public string Firstname { get; set; }
        public string? UserName { get; set; }
       public string? RoleId { get; set; }

        public string Lastname { get; set; }
       
        public string Email { get; set; }
  
        public string PhoneNumber { get; set; }
    

        public string Password { get; set; }


        public string ConfirmPassword { get; set; }
    }
}
