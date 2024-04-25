using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.UserDTOs
{
    public class UserResetPasswordDTO
    {
        [Required(ErrorMessage = "Istifadeci Id bos Ola bilmez")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Yeni parolu daxil edin")]
        [Compare("NewPasswordConfirm", ErrorMessage = "Yeni parol ile tesdiqleme parolu ust uste dusmur!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Yeni paralun tesdiqlenmesini  daxil edin")]
        public string Token { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
