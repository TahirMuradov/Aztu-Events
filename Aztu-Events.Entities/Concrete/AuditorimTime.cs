using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class AuditorimTime
    {
        public Guid Id { get; set; }
        public Guid TimeId { get; set; }
        public Time Time { get; set; }
        public Guid AudutoriumId { get; set; }
        public Auditorium Audutorium { get; set; }
    }
}
