using EventSourcingService.DTO;
using EventSourcingService.Mapper;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientSchedulingEventController : ControllerBase
    {
        private readonly IEventStorePatientSchedulingService _eventStorePatientSchedulingService;

        public PatientSchedulingEventController(IEventStorePatientSchedulingService eventStorePatientSchedulingService)
        {
            _eventStorePatientSchedulingService = eventStorePatientSchedulingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _eventStorePatientSchedulingService.GetAll().Select(e => e.ToPatientSchedulingEventDTO());
            return Ok(exampleEventStores);
        }

        [HttpPost]
        public ActionResult Add(PatientSchedulingEventDTO schedulingEventDTO)
        {
            _eventStorePatientSchedulingService.Add(schedulingEventDTO);
            return NoContent();
        }

    }
}
