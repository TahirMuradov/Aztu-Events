using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Entities.DTOs.CommentDTOs
{
    public class GetCommentForAdminDTO
    {
        public string CommentId { get; set; }
        public string ConferenceId { get; set; }
        public string ConferenceName { get; set; }
        public bool IsSafe { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Content { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
