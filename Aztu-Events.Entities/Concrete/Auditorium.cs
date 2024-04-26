using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class Auditorium
    {
        public Guid Id { get; set; }
        public string AudutoriyaNumber { get; set; }
        public int AuditoryCapacity { get; set; }
        public List<Confrans>? Confrances { get; set; }

        public List<AuditorimTime>? AudutorimTimes { get; set; }
    }
}
