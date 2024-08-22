using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennStateSoft.Data.Models;

namespace PennStateSoft.Data
{
    public class NotificationContext : DbContext
    {
        public NotificationContext (DbContextOptions<NotificationContext> options)
            : base(options)
        {
        }

        public DbSet<PennStateSoft.Data.Models.Notification> Notification { get; set; } = default!;
    }
}
