namespace PennStateSoft.Data.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Subject { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
        public byte[]? Description { get; set; }
        public List<ComplaintReply>? ComplaintReplies { get; set; }
    }
}
