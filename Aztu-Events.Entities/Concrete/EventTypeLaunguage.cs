using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class EventTypeLaunguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string TypeContent { get; set; }
        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
