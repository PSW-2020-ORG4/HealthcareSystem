using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using System.Collections.Generic;

namespace EventSourcingService.Service.GraphicalEditor
{
    public class FloorChangeService : IFloorChangeService
    {
        private readonly IDomainEventRepository<FloorChangeEvent> _floorChangeRepository;

        public FloorChangeService(IDomainEventRepository<FloorChangeEvent> floorChangeRepository)
        {
            _floorChangeRepository = floorChangeRepository;
        }

        public IEnumerable<FloorChangeEvent> GetAll()
        {
            return _floorChangeRepository.GetAll();
        }

        public void Add(FloorChangeEvent floorChangeEvent)
        {
            _floorChangeRepository.Add(floorChangeEvent);
        }
    }
}
