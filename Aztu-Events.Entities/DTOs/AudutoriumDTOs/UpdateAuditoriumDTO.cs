using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AudutoriumDTOs
{
    public class UpdateAuditoriumDTO
    {
   
        public Guid AuditoriumId { get; set; }

 
        public string AudutoriyaNumber { get; set; }

       
        public int AuditoriumCapacity { get; set; }
    }
}
