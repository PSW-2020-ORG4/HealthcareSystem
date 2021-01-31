using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using System.Collections.Generic;

namespace EventSourcingService.Service.GraphicalEditor
{
    public class RoomSelectionService : IRoomSelectionService
    {
        private readonly IDomainEventRepository<RoomSelectionEvent> _roomSelectionRepository;

        public RoomSelectionService(IDomainEventRepository<RoomSelectionEvent> roomSelectionRepository)
        {
            _roomSelectionRepository = roomSelectionRepository;
        }

        public IEnumerable<RoomSelectionEvent> GetAll()
        {
            return _roomSelectionRepository.GetAll();
        }

        public void Add(RoomSelectionEvent roomSelectionEvent)
        {
            _roomSelectionRepository.Add(roomSelectionEvent);
        }
    }
}
