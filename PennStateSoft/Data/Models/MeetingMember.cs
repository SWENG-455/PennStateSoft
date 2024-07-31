namespace PennStateSoft.Data.Models
{
    public class MeetingMember
    {
        public int Id { get; set; }
        public ApplicationUser? User { get; set; }
        public int MeetingId { get; set; }
    }
}
