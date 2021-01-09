using EventSourcingService.Model;
using EventSourcingService.Repository;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStoreController : ControllerBase
    {
        private readonly IDomainEventRepository<ExampleEvent> _exampleEventRepository;

        public EventStoreController(IDomainEventRepository<ExampleEvent> exampleEventRepository)
        {
            _exampleEventRepository = exampleEventRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _exampleEventRepository.GetAll();
            return Ok(exampleEventStores);
            //var eventStores = _eventStoreService.GetAllEvents();
            //return Ok(eventStores);
        }

        [HttpPost]
        public ActionResult Add(ExampleEvent exampleEvent)
        {
            _exampleEventRepository.Add(exampleEvent);

            //_eventStoreService.Add(domainEvent);
            return NoContent();
        }



    }
}
