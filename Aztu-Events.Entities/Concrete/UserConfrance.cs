using Aztu_Events.Entities.DTOs.Conferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class UserConfrance
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid ConfransId { get; set; }
        public Confrans Confrans { get; set; }
    }
}
