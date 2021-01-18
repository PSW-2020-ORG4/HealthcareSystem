using EventSourcingService.DTO;
using EventSourcingService.Mapper;
using EventSourcingService.Model;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventStoreExampleService _eventStoreExampleService;

        public EventController(IEventStoreExampleService eventStoreExampleService)
        {
            _eventStoreExampleService = eventStoreExampleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _eventStoreExampleService.GetAll().Select(e => e.ToStatisticEventDTO()); ;
            return Ok(exampleEventStores);
        }

        [HttpPost]
        public ActionResult Add(StatisticEventDTO statisticEventDTO)
        {
            _eventStoreExampleService.Add(statisticEventDTO);
            return NoContent();
        }

    }
}
