using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Settings;
using RestSharp;
using ScheduleService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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
            parameters.EarliestDateTime = InitializeEarliestTime(parameters.EarliestDateTime);
            parameters.LatestDateTime = InitializeLatestTime(parameters.LatestDateTime);

            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination/search-free/basic");
            request.AddJsonBody(parameters);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
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
            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination/search-free/advanced");
            request.AddJsonBody(parameters);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
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
