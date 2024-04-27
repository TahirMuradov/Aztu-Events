using Aztu_Events.Entities.EnumClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.Conferences
{
    public class ConferenceGetAdminListDTO
    {
        public Guid Id { get; set; }
        public string ConferenceName { get; set; }
        public string ConferenceContent { get; set; }
        public string LangCode { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserEmail { get; set; }
        public string UserFullname { get; set; }
        public Guid AudutoriumId { get; set; }
        public string AudutoriumName { get; set; }
        public ConferanceStatus Status { get; set; }
        public string ImgUrl { get; set; }
    }
}
