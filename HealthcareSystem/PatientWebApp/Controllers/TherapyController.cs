using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.SearchSpecification.TherapySearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using Model.Users;
using Newtonsoft.Json;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TherapyController : ControllerBase
    {
        private readonly ITherapyService _therapyService;
        public TherapyController(ITherapyService therapyService)
        {
            _therapyService = therapyService;
        }

        /// <summary>
        /// /getting therapies linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public ActionResult GetTherapiesByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient("http://localhost:" + 65428);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/therapy");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        /// <summary>
        /// /getting searched therapies linked to a certain patient
        /// </summary>
        /// <param name="therapySearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("advance-search")]
        public ActionResult AdvanceSearchTherapies(TherapySearchDTO therapySearchDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient("http://localhost:" + 65428);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/therapy/search", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(therapySearchDTO);
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<IEnumerable<TherapyDTO>>(response.Content));

        }
    }
}
