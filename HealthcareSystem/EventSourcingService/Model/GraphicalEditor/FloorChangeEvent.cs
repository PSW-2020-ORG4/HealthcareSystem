using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class FloorChangeEvent : DomainEvent
    {
        public String Username { get; private set; }
        public int BuildingNumber { get; private set; }
        public int Floor { get; private set; }
    }
}
