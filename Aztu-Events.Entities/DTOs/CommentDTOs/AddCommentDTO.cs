using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.CommentDTOs
{
    public class AddCommentDTO
    {
        public string UserId { get; set; }
        public string ConferenceId { get; set; }
        public string Content { get; set; }
    }
}
