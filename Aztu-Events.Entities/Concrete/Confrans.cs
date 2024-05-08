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
  
       
        public Guid TimeId { get; set; }
        public Time Time { get; set; }
        public ConferanceStatus Status { get; set; }
        public Guid AudutoriumId { get; set; }
        public Auditorium Audutorium { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        public List<ConfranceLaunguage> ConfranceLaunguages { get; set; }
        public List<SpecialGuest> SpecialGuests { get; set; }
    }
}
