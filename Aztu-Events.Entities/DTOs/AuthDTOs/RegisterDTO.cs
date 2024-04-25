using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Adınızı Daxil Edin!")]

        public string Firstname { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Soyadınızı Daxil Edin!")]

        public string Lastname { get; set; }
        [Required(ErrorMessage = "Email Daxil Edin!")]

        [EmailAddress(ErrorMessage = "Email duzgun yazin!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Əlaqə Nömrəsi daxil edin!")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Əlaqə nömrəsi formatı düzgün deyil")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Compare("ConfirmPassword", ErrorMessage = "Təsdiqləmə parolu ilə parol üst üstə düşmür.Yenidən Yoxlayın!")]
        [Required(ErrorMessage = "Parol boş ola bilməz")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Təsdiqləmə Parolu boş ola bilməz!")]

        public string ConfirmPassword { get; set; }
    }
}
