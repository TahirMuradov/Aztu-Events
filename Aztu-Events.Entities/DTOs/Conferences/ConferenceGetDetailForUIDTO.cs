﻿using Aztu_Events.Entities.DTOs.CommentDTOs;

namespace Aztu_Events.Entities.DTOs.Conferences
{
    public class ConferenceGetDetailForUIDTO
    {
        public Guid Id { get; set; }
        public string ConferenceName { get; set; }
        public string ConferenceContent { get; set; }
        public string CategoryName { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartedDate { get; set; }
        public TimeOnly EndDate { get; set; }
        public List<string> UsersId { get; set; }
        public bool IsSavedPdf { get; set; }
        public string UserEmail { get; set; }
        public string UserFullname { get; set; }
        public string AudutoriumName { get; set; }   
        public string ImgUrl { get; set; }
        public string PdfUrl { get; set; }
        public int CurrentPerson { get; set; }
        public List<GetCommentForUIDTO>? Comments { get; set; }
        public List<GETConfranceSpecialGuestDTO> specialGuests { get; set; }
    }
}
