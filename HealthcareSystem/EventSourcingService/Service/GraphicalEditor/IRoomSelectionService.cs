using EventSourcingService.Model.GraphicalEditor;
using System.Collections.Generic;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IRoomSelectionService
    {
        public IEnumerable<RoomSelectionEvent> GetAll();
        public void Add(RoomSelectionEvent mapObjectSelection);
    }
}
