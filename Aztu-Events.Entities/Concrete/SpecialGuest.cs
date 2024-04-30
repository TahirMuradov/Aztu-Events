﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class SpecialGuest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool SendEmail { get; set; }
        public Guid ConfransId { get; set; }
        public Confrans Confrans { get; set; }
    }
}
