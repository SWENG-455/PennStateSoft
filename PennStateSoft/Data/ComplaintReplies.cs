using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennStateSoft.Data.Models;

namespace PennStateSoft.Data
{
    public class ComplaintReplies : DbContext
    {
        public ComplaintReplies (DbContextOptions<ComplaintReplies> options)
            : base(options)
        {
        }

        public DbSet<ComplaintReply> ComplaintReply { get; set; } = default!;
    }
}
