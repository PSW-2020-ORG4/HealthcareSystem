using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model.GraphicalEditor
{
    public class BuildingSelectionEvent : DomainEvent
    {
        public String Username { get; set; }
        public int BuildingNumber { get; set; }

        public BuildingSelectionEvent() { }
    }
}
