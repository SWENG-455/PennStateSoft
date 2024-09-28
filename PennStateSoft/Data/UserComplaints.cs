using Microsoft.EntityFrameworkCore;
using PennStateSoft.Data.Models;

namespace PennStateSoft.Data
{
    public class UserComplaints : DbContext
    {
        public UserComplaints (DbContextOptions<UserComplaints> options)
            : base(options)
        {
        }

        public DbSet<Complaint> Complaint { get; set; } = default!;
        public DbSet<ComplaintReply> ComplaintReply { get; set; } = default!;
        public DbSet<PennStateSoft.Data.Models.ReplyReply> ReplyReply { get; set; } = default!;
        public DbSet<PennStateSoft.Data.Models.ComplaintReplyReply> ComplaintReplyReply { get; set; } = default!;
    }
}
