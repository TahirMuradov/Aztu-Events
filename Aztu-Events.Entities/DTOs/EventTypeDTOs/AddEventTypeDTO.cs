﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.EventTypeDTOs
{
    public class AddEventTypeDTO
    {
        public List<string> LangCode { get; set; }
        public List<string> Content { get; set; }

    }
}
