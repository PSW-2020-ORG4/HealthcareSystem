using EventSourcingService.Model.GraphicalEditor;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Model
{
    public class EventSourcingDbContext : DbContext
    {
        public DbSet<PatientStartSchedulingEvent> PatientStartSchedulingEvents { get; set; }
        public DbSet<PatientStepSchedulingEvent> PatientSchedulingEvents { get; set; }
        public DbSet<PatientEndSchedulingEvent> PatientEndSchedulingEvents { get; set; }

        public DbSet<FloorChangeEvent> FloorChangeEvent { get; set; }
        public DbSet<RoomSelectionEvent> RoomSelectionEvent { get; set; }
        public DbSet<BuildingSelectionEvent> BuildingSelectionEvent { get; set; }

        public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DomainEvent>().HasIndex(e => e.TriggerTime);
        }

    }
}
