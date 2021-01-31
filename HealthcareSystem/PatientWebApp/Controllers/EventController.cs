using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public EventController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.EventSourcingServiceUrl, "/api/patientSchedulingEvent", Method.GET);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("start")]
        public IActionResult AddStartEvent(AddStartSchedulingEventDTO startSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/patientSchedulingEvent/start", startSchedulingEventDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("step")]
        public IActionResult AddStepEvent(AddStepSchedulingEventDTO stepSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/patientSchedulingEvent/step", stepSchedulingEventDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("end")]
        public IActionResult AddEndEvent(AddEndSchedulingEventDTO endSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/patientSchedulingEvent/end", endSchedulingEventDTO);
        }

    }
}
