using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service.GraphicalEditor
{
    public class MapObjectSelectionService : IMapObjectSelectionService
    {
        private readonly IDomainEventRepository<MapObjectSelectionEvent> _mapObjectSelectionEventRepository;

        public MapObjectSelectionService(IDomainEventRepository<MapObjectSelectionEvent> exampleEventRepository)
        {
            _mapObjectSelectionEventRepository = exampleEventRepository;
        }

        public IEnumerable<MapObjectSelectionEvent> GetAll()
        {
            return _mapObjectSelectionEventRepository.GetAll();
        }

        public void Add(MapObjectSelectionEvent mapObjectSelectionEvent)
        {
            _mapObjectSelectionEventRepository.Add(mapObjectSelectionEvent);
        }
    }
}
