using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class AlertLaunguage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LangCode { get; set; }
        public Guid AlertId { get; set; }
        public Alert Alert { get; set; }
    }
}
