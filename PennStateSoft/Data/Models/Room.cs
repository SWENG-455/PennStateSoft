using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace PennStateSoft.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
    }
}
