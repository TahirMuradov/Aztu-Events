using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class ConfranceLaunguage
    {
        public Guid Id { get; set; }
        public string ConfransName { get; set; }
        public string ConfransContent { get; set; }
        public string LangCode { get; set; }
        public Guid ConfransId { get; set; }
        public Confrans Confrans { get; set; }
    }
}
