using Microsoft.EntityFrameworkCore;

namespace PennStateSoft.Data
{
    public class UserComplaints : DbContext
    {
        public UserComplaints (DbContextOptions<UserComplaints> options)
            : base(options)
        {
        }

        public DbSet<Complaint> Complaint { get; set; } = default!;
    }
}
