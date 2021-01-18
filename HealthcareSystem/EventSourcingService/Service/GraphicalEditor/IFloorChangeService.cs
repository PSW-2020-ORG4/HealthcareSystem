using EventSourcingService.Model.GraphicalEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IFloorChangeService
    {
        public IEnumerable<FloorChangeEvent> GetAll();
        public void Add(FloorChangeEvent floorChangeEvent);
    }
}
