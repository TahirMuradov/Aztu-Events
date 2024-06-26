﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.Concrete
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserConfrance>? userConfrances { get; set; }
        public List<SavePdf >?Pdfs { get; set; }
        public List<Confrans>? Confrans { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
