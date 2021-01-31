using EventSourcingService.Model.GraphicalEditor;
using System.Collections.Generic;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IBuildingSelectionService
    {
        public IEnumerable<BuildingSelectionEvent> GetAll();
        public void Add(BuildingSelectionEvent mapObjectSelection);
    }
}
