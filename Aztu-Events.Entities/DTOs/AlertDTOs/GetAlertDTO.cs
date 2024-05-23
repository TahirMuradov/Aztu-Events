using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.AlertDTOs
{
    public class GetAlertDTO
    {
        public string AlertId { get; set; }
        public string AlertContent { get; set; }
        public string? ConferenceId { get; set; }
        public bool ForUser { get; set; }
        public string? UserId { get; set; }
    }
}
