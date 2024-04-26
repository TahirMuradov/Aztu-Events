using Aztu_Events.Entities.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class Confrans
    {
        public Guid Id { get; set; }
    
        public string ImgUrl { get; set; }
        public List<string>? specialGuestsEmail { get; set; }
        public List<string>? specialGuestsName { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConferanceStatus Status { get; set; }
        public Guid AudutoriumId { get; set; }
        public Auditorium Audutorium { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
