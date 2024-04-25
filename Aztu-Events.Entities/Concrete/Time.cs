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
        public DateTime DateTime { get; set; }
     public List<AudutorimTime> AudutorimTimes { get; set;}
    }
}
