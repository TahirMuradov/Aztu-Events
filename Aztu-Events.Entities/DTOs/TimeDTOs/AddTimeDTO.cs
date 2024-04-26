using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.TimeDTOs
{
    public class AddTimeDTO
    {
        public DateTime Time { get; set; }
        public string AuditoriumId { get; set; }

    }
}
