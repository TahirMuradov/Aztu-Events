using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class EventType
    {
        public Guid Id { get; set; }
        public List<EventTypeLaunguage> EventTypeLaunguage { get; set; }
        public List<Confrans>? Confrans { get; set; }
        
    }
}
