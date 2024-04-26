using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AudutoriumDTOs
{
    public class GetAuditoriumDTO
    {
        public Guid AuditoriumId { get; set; }
        public string AudutoriumNumber { get; set; }
        public int AuditoriumCapacity { get; set; }
        public List<DateTime> AuditoriumFreeTimes { get; set; }

    }
}
