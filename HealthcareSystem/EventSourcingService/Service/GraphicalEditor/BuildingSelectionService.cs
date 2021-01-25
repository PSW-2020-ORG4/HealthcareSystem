using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public class BuildingSelectionService : IBuildingSelectionService
    {
        private readonly IDomainEventRepository<BuildingSelectionEvent> _buildingSelectionRepository;

        public BuildingSelectionService(IDomainEventRepository<BuildingSelectionEvent> buildingSelectionRepository)
        {
            _buildingSelectionRepository = buildingSelectionRepository;
        }

        public IEnumerable<BuildingSelectionEvent> GetAll()
        {
            return _buildingSelectionRepository.GetAll();
        }

        public void Add(BuildingSelectionEvent buildingSelectionEvent)
        {
            _buildingSelectionRepository.Add(buildingSelectionEvent);
        }
    }
}
