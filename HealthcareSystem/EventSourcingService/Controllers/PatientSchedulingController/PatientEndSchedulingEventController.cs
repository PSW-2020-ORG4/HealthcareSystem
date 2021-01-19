using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingService.DTO;
using EventSourcingService.Mapper;
using EventSourcingService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientEndSchedulingEventController : ControllerBase
    {
        private readonly IEventStorePatientEndSchedulingService _eventStorePatientStartSchedulingService;
        public PatientEndSchedulingEventController(IEventStorePatientEndSchedulingService eventStorePatientEndSchedulingService)
        {
            _eventStorePatientStartSchedulingService = eventStorePatientEndSchedulingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exampleEventStores = _eventStorePatientStartSchedulingService.GetAll().Select(e => e.ToPatientEndSchedulingEventDTO());
            return Ok(exampleEventStores);
        }

        [HttpPost]
        public ActionResult Add(PatientEndSchedulingEventDTO endSchedulingEventDTO)
        {
            _eventStorePatientStartSchedulingService.Add(endSchedulingEventDTO);
            return NoContent();
        }
    }
}
