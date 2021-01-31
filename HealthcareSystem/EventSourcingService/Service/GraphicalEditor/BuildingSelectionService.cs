using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using System.Collections.Generic;

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
