using EventSourcingService.Model.GraphicalEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IRoomSelectionService
    {
        public IEnumerable<RoomSelectionEvent> GetAll();
        public void Add(RoomSelectionEvent mapObjectSelection);
    }
}
