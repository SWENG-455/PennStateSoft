using Microsoft.EntityFrameworkCore;
using PennStateSoft.Data.Models;

namespace PennStateSoft.Data
{
    public class MeetingContext : DbContext
    {
        public MeetingContext (DbContextOptions<MeetingContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Room { get; set; } = default!;
    }
}
