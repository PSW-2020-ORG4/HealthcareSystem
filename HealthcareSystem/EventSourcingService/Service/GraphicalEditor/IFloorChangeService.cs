using EventSourcingService.Model.GraphicalEditor;
using System.Collections.Generic;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IFloorChangeService
    {
        public IEnumerable<FloorChangeEvent> GetAll();
        public void Add(FloorChangeEvent floorChangeEvent);
    }
}
