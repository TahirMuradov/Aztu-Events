using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.TimeDTOs
{
    public class GetTimeDTO
    {
        public string TimeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartedTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid  AuditoriumId { get; set; }
    }
}
