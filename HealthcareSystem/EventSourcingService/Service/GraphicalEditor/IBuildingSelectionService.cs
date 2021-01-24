using EventSourcingService.Model.GraphicalEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public interface IBuildingSelectionService
    {
        public IEnumerable<BuildingSelectionEvent> GetAll();
        public void Add(BuildingSelectionEvent mapObjectSelection);
    }
}
