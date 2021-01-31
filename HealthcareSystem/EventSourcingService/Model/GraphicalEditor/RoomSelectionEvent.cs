using System;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class RoomSelectionEvent : DomainEvent
    {
        public String Username { get; set; }
        public int RoomNumber { get; set; }
        public RoomSelectionEvent() { }

    }
}
