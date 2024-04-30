using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AudutoriumDTOs
{
    public class AddAuditoriumDTO
    {
        [Required(ErrorMessage = "Audutoriya nömrəsi boş ola bilməz.")]
        [StringLength(10, ErrorMessage = "Audutoriya nömrəsi {0} simvoldan çox ola bilməz.")]
        [Display(Name = "Audutoriya Nömrəsi")]
        public string AudutoriyaNumber { get; set; }

        [Required(ErrorMessage = "Tutum sayı boş ola bilməz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Tutum sayı 1-dən böyük olmalıdır.")]
        [Display(Name = "Tutum")]
        public int AuditoryCapacity { get; set; }
    }
}
