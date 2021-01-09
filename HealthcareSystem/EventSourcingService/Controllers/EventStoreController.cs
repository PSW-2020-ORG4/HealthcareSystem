using EventSourcingService.Model;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStoreController : ControllerBase
    {
        private readonly IEventStoreService _eventStoreService;

        public EventStoreController(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var eventStores = _eventStoreService.GetAllEvents();
            return Ok(eventStores);
        }

        [HttpPost]
        public ActionResult Add(CustomEvent customEvent)
        {
            _eventStoreService.Add(customEvent);
            return NoContent();
        }

    }
}
