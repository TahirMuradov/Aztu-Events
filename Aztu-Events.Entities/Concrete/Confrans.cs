using Aztu_Events.Entities.EnumClass;

namespace Aztu_Events.Entities.Concrete
{
    public class Confrans
    {
        public Guid Id { get; set; }

        public string ImgUrl { get; set; }

        public bool IsFeatured { get; set; }
        public Guid TimeId { get; set; }
        public Time Time { get; set; }
        public ConferanceStatus Status { get; set; }
        public Guid AudutoriumId { get; set; }
        public Auditorium Audutorium { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<SavePdf>? SavePdfs { get; set; }
        public string PdfUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public List<UserConfrance>? userConfrances { get; set; }
        public List<ConfranceLaunguage> ConfranceLaunguages { get; set; }
        public List<SpecialGuest> SpecialGuests { get; set; }
        public List<Comment>?Comments { get; set; }
    }
}
