using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AudutoriumDTOs
{
    public class GetAuditoriumDTO
    {
        [Required(ErrorMessage ="Id boş ola bilməz!")]
        public Guid AuditoriumId { get; set; }

        [Required(ErrorMessage = "Audutoriya nömrəsi boş ola bilməz.")]
        [StringLength(50, ErrorMessage = "Audutoriya nömrəsi {0} simvoldan çox ola bilməz.")]
        [Display(Name = "Audutoriya Nömrəsi")]
        public string AudutoriyaNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Auditoria Tutumu 1-dən böyük olmalıdır.")]
        [Display(Name = "Auditoria Tutumu")]
        public int AuditoriumCapacity { get; set; }

        public List<DateOnly>? Date { get; set; }
        public List<TimeOnly>? StartedTime { get; set; }
        public List<TimeOnly>? EndTime { get; set; }

    }
}
