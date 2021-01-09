using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Model
{
    public class EventSourcingDbContext : DbContext
    {

        public DbSet<DomainEvent> CustomEvents { get; set; }

        public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }

    }
}
