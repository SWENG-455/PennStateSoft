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
        public DbSet<Meeting> Meeting { get; set; } = default!;
        public DbSet<MeetingMember> Members { get; set; } = default!;
    }
}
