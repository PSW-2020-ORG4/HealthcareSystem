using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return RequestAdapter.SendRequestWithoutBody("http://localhost:58828", "/api/patientSchedulingEvent", Method.GET);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("start")]
        public IActionResult AddStartEvent(AddStartSchedulingEventDTO startSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody("http://localhost:58828", "/api/patientSchedulingEvent/start", startSchedulingEventDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("step")]
        public IActionResult AddStepEvent(AddStepSchedulingEventDTO stepSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody("http://localhost:58828", "/api/patientSchedulingEvent/step", stepSchedulingEventDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("end")]
        public IActionResult AddEndEvent(AddEndSchedulingEventDTO endSchedulingEventDTO)
        {
            return RequestAdapter.SendRequestWithBody("http://localhost:58828", "/api/patientSchedulingEvent/end", endSchedulingEventDTO);
        }

    }
}
