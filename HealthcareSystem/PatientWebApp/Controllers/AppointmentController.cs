using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.Settings;
using ScheduleService.DTO;
using System;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public AppointmentController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of basic search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("basic-search")]
        public IActionResult BasicSearchAppointments(BasicSearchDTO parameters)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            parameters.PatientJmbg = patientJmbg;
            parameters.EarliestDateTime = InitializeEarliestTime(parameters.EarliestDateTime);
            parameters.LatestDateTime = InitializeLatestTime(parameters.LatestDateTime);

            return RequestAdapter.SendRequestWithBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/search-free/basic", parameters);
        }
        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of priority search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("priority-search")]
        public IActionResult PrioritySearchAppointments(AdvancedSearchDTO parameters)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            parameters.InitialParameters.PatientJmbg = patientJmbg;
            parameters.InitialParameters.EarliestDateTime = InitializeEarliestTime(
                parameters.InitialParameters.EarliestDateTime);
            parameters.InitialParameters.LatestDateTime = InitializeLatestTime(
                parameters.InitialParameters.LatestDateTime);

            return RequestAdapter.SendRequestWithBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/search-free/advanced", parameters);
        }

        private DateTime InitializeEarliestTime(DateTime earliest)
        {
            return new DateTime(earliest.Year, earliest.Month, earliest.Day, 7, 0, 0);
        }

        private DateTime InitializeLatestTime(DateTime latest)
        {
            return new DateTime(latest.Year, latest.Month, latest.Day, 17, 0, 0);
        }
    }
}
