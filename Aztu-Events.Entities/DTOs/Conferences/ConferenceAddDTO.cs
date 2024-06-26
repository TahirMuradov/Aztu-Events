﻿namespace Aztu_Events.Entities.DTOs.Conferences
{
    public class ConferenceAddDTO
    {
        public List<string> ConferenceName { get; set; }
        public List<string> ConferenceContent { get; set; }
        public List<string> LangCode { get; set; }
        public List<string>? specialGuestsEmail { get; set; }
        public List<string>? specialGuestsName { get; set; }
        public bool? IsFeatured { get; set; }
        public string ? PdfUrl { get; set; }
        public string CategoryId { get; set; }
        public string EventTypeId { get; set; }
        public DateOnly? Day { get; set; }
        public TimeOnly? StartedDate { get; set; }
        public TimeOnly? EndDate { get; set; }
        public string ImgUrl { get; set; }
        public string UserId { get; set; }
        public Guid? AudutoriumId { get; set; }
    }
}
