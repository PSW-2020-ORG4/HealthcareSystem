using EventSourcingService.Model;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStoreExampleController : ControllerBase
    {
        private readonly IEventStoreExampleService _eventStoreExampleService;

        public EventStoreExampleController(IEventStoreExampleService eventStoreExampleService)
        {
            _eventStoreExampleService = eventStoreExampleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _eventStoreExampleService.GetAll();
            return Ok(exampleEventStores);
        }

        [HttpPost]
        public ActionResult Add(ExampleEvent exampleEvent)
        {
            _eventStoreExampleService.Add(exampleEvent);
            return NoContent();
        }

    }
}
