using System.ComponentModel.DataAnnotations;

namespace PennStateSoft.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int MeetingId { get; set; }
        public double Price { get; set; } = 100;
    }
}
