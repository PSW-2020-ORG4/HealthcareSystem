using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Model
{
    public class EventSourcingDbContext : DbContext
    {
        public DbSet<PatientStartSchedulingEvent> PatientStartSchedulingEvents { get; set; }
        public DbSet<PatientSchedulingEvent> PatientSchedulingEvents { get; set; }
        public DbSet<PatientEndSchedulingEvent> PatientEndSchedulingEvents { get; set; }

        public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DomainEvent>().HasIndex(e => e.TriggerTime);
        }

    }
}
