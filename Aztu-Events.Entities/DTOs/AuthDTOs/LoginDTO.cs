using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email bos ola bilmez!")]
        [EmailAddress(ErrorMessage = "Email adress formati duzgun deyil!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parol bos ola bilmez!")]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
