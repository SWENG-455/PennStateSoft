using System.ComponentModel.DataAnnotations;

namespace PennStateSoft.Data.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        public int RoomId { get; set; }
        public int Duration { get; set; } = 1;
        public List<MeetingMember>? Members { get; set; }
    }
}
