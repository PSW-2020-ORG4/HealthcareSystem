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
        private readonly IEventStorePatientStartSchedulingService _patientStartSchedulingService;
        private readonly IEventStorePatientStepSchedulingService _patientStepSchedulingService;
        private readonly IEventStorePatientEndSchedulingService _patientEndSchedulingService;

        public PatientSchedulingEventController(IEventStorePatientStartSchedulingService patientStartSchedulingService,
                                                IEventStorePatientStepSchedulingService patientStepSchedulingService,
                                                IEventStorePatientEndSchedulingService patientEndSchedulingService)
        {
            _patientStartSchedulingService = patientStartSchedulingService;
            _patientStepSchedulingService = patientStepSchedulingService;
            _patientEndSchedulingService = patientEndSchedulingService;
        }

        [HttpPost("start")]
        public IActionResult Add()
        {
            return Ok(_patientStartSchedulingService.Add().ToPatientStartSchedulingEventDTO());
        }

        [HttpPost("step")]
        public IActionResult Add(PatientStepSchedulingEventDTO schedulingEventDTO)
        {
            _patientStepSchedulingService.Add(schedulingEventDTO);
            return NoContent();
        }

        [HttpPost("end")]
        public IActionResult Add(PatientEndSchedulingEventDTO endSchedulingEventDTO)
        {
            _patientEndSchedulingService.Add(endSchedulingEventDTO);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllStatistic()
        {
            PatientSchedulingStatisticDTO schedulingStatisticDTO = new PatientSchedulingStatisticDTO();

            schedulingStatisticDTO.successfulSchedulingDuration = _patientEndSchedulingService.SuccessfulSchedulingDuration();

            return Ok(schedulingStatisticDTO);
        }
    }
}
