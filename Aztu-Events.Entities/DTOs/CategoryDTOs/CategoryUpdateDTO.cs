using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public string CategoryId { get; set; }
        public List<string> LangCode { get; set; }
        public List<string> Content { get; set; }
    }
}
