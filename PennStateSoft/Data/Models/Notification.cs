namespace PennStateSoft.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int ReferenceID { get; set; }
        public string? Author { get; set; }
        public string? Recipient { get; set; }
        public string? Message { get; set; }
        public string? LinkTo { get; set; }
        public string? Link { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Read { get; set; }
        public bool Selected { get; set; }
    }
}
