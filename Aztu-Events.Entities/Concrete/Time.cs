using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class Time
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartedTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool UpdateTime { get; set; }
        public Guid ConfransId { get; set; }

        public Confrans Confrans { get; set; }
        public Guid AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
    }
}
