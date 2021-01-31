using System;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class FloorChangeEvent : DomainEvent
    {
        public String Username { get; set; }
        public int BuildingNumber { get; set; }
        public int Floor { get; set; }

        public FloorChangeEvent() { }
    }
}
