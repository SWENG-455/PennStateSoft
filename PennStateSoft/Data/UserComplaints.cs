using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennStateSoft;

namespace PennStateSoft.Data
{
    public class UserComplaints : DbContext
    {
        public UserComplaints (DbContextOptions<UserComplaints> options)
            : base(options)
        {
        }

        public DbSet<PennStateSoft.Complaint> Complaint { get; set; } = default!;
    }
}
