using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool AlertSeen { get; set; }
        public bool IsSafe { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid ConfransId { get; set; }
        public Confrans Confrans { get; set; }

    }
}
