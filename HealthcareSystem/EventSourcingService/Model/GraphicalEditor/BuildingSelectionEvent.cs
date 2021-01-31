using System;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class BuildingSelectionEvent : DomainEvent
    {
        public String Username { get; set; }
        public int BuildingNumber { get; set; }

        public BuildingSelectionEvent() { }
    }
}
