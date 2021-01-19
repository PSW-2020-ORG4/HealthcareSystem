using EventSourcingService.DTO;
using EventSourcingService.Mapper;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientStartSchedulingEventController : ControllerBase
    {
        private readonly IEventStorePatientStartSchedulingService _eventStorePatientStartSchedulingService;

        public PatientStartSchedulingEventController(IEventStorePatientStartSchedulingService eventStorePatientStartSchedulingService)
        {
            _eventStorePatientStartSchedulingService = eventStorePatientStartSchedulingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _eventStorePatientStartSchedulingService.GetAll().Select(e => e.ToPatientStartSchedulingEventDTO());
            return Ok(exampleEventStores);
        }

        [HttpPost]
        public ActionResult Add()
        {
            return Ok(_eventStorePatientStartSchedulingService.Add().ToPatientStartSchedulingEventDTO());
        }

    }
}
