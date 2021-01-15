using EventSourcingService.Model.GraphicalEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IMapObjectSelectionService
    {
        public IEnumerable<MapObjectSelectionEvent> GetAll();
        public void Add(MapObjectSelectionEvent mapObjectSelection);
    }
}
