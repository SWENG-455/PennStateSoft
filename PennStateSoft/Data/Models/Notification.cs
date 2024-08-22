namespace PennStateSoft.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReferenceID { get; set; }
        public string? Author { get; set; }
        public string? Message { get; set; }
        public string? LinkTo { get; set; }
        public string? Link { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsReferenceOwner { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
