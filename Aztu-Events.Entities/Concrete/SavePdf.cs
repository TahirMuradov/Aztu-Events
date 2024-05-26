namespace Aztu_Events.Entities.Concrete
{
    public class SavePdf
    {
        public Guid Id { get; set; }
        public Guid ConferenceId { get; set; }
        public Confrans Conference { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
