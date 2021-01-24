using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class RoomSelectionEvent : DomainEvent
    {
        public String Username { get; set; }
        public int RoomNumber { get; set; }
        public RoomSelectionEvent() { }
    }
}
