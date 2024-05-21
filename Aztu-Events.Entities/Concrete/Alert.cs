using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class Alert
    {
        public Guid Id { get; set; }
        public bool ForUser { get; set; }
       
        public string? UserId { get; set; }
        public string? ConferenceId { get; set; }
       
        public AlertLaunguage AlertLaunguage { get; set; }
    }
}
