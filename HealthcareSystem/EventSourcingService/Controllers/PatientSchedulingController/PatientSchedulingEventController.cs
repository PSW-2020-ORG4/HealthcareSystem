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
        public IActionResult Add(PatientStartSchedulingEventDTO startSchedulingEventDTO)
        {
            return Ok(_patientStartSchedulingService.Add(startSchedulingEventDTO).ToPatientStartSchedulingEventDTO());
        }

        [HttpPost("step")]
        public IActionResult Add(PatientStepSchedulingEventDTO stepSchedulingEventDTO)
        {
            _patientStepSchedulingService.Add(stepSchedulingEventDTO);
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
            schedulingStatisticDTO.successfulSchedulingGenderStatistic = _patientEndSchedulingService.SuccessfulSchedulingGenderStatistic();
            schedulingStatisticDTO.successfulSchedulingAgeStatistic = _patientEndSchedulingService.SuccessfulSchedulingAgeStatistic();
            schedulingStatisticDTO.closedSchedulingStepStatistic = _patientStepSchedulingService.ClosedSchedulingStepStatistic();
            schedulingStatisticDTO.previousSchedulingStepStatistic = _patientStepSchedulingService.PreviousSchedulingStepStatistic();
            schedulingStatisticDTO.successfulSchedulingAgeDuration = _patientEndSchedulingService.SuccessfulSchedulingAgeDuration();
            schedulingStatisticDTO.successfulSchedulingGenderDuration = _patientEndSchedulingService.SuccessfulSchedulingGenderDuration();
            schedulingStatisticDTO.unsuccessfulSchedulingAgeDuration = _patientEndSchedulingService.UnsuccessfulSchedulingAgeDuration();
            schedulingStatisticDTO.unsuccessfulSchedulingGenderDuration = _patientEndSchedulingService.UnsuccessfulSchedulingGenderDuration();
            schedulingStatisticDTO.schedulingStepsStatistic = _patientStepSchedulingService.SchedulingStepsStatistic();
            schedulingStatisticDTO.successfulAndUnsuccessfulSchedulingDTO = _patientEndSchedulingService.SuccessfulAndUnsuccessfulScheduling();

            return Ok(schedulingStatisticDTO);
        }
    }
}
