using Backend.Model.Exceptions;
using Backend.Model.PerformingExamination;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;
using PatientWebApp.Validators;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly ExaminationValidator _examinationValidator;
        private readonly ServiceSettings _serviceSettings;

        public ExaminationController(IExaminationService examinationService, IOptions<ServiceSettings> serviceSettings)
        {
            _examinationService = examinationService;
            _examinationValidator = new ExaminationValidator(_examinationService);
            _serviceSettings = serviceSettings.Value;
        }

        /// <summary>
        /// /getting examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("by-patient")]
        public ActionResult GetExaminationsByPatient()
        {
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// /getting searched examinations linked to a certain patient
        /// </summary>
        /// <param name="examinationSearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("advance-search")]
        public ActionResult AdvanceSearchExaminations(ExaminationSearchDTO examinationSearchDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.PatientServiceUrl);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/examination/search", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(examinationSearchDTO);

            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// <summary>
        /// / updating examiantionStatus (property: ExaminationStatus) to CANCELED
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request), if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("cancel/{id}")]
        public ActionResult CancelExamination(int id)
        {
            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination/" + id + "/cancel", Method.POST);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// /getting canceled examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("cancelled")]
        public ActionResult GetCanceledExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination/canceled/patient/" + patientJmbg);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// <summary>
        /// /getting previous examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("previous")]
        public ActionResult GetPreviousExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.PatientServiceUrl);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/examination");
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

           /* var newClient = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var newRequest = new RestRequest("/api/examination/finished/patient/" + patientJmbg );
            var newResponse = newClient.Execute(newRequest);

            var newContentResult = new ContentResult();
            newContentResult.Content = newResponse.Content;
            newContentResult.ContentType = "application/json";
            newContentResult.StatusCode = (int)newResponse.StatusCode;
            
            return newContentResult;*/
            return contentResult;
        }

        /// <summary>
        /// /getting following examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("following")]
        public ActionResult GetFollowingExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination/created/patient/" + patientJmbg);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// <summary>
        /// /getting one examination by id
        /// </summary>
        /// <param name="id">examination id</param>
        /// <returns>if alright returns code 200(Ok), if returned value is null returns 404, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("{id}")]
        public IActionResult GetExaminationById(int id)
        {
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                Examination examination = _examinationService.GetExaminationById(id);
                if (!patientJmbg.Equals(examination.PatientCard.PatientJmbg))
                {
                    return StatusCode(403, "Patient tried to get someone else's examination");
                }
                ExaminationDTO examinationDTO = new ExaminationDTO();
                examinationDTO = ExaminationMapper.ExaminationToExaminationDTO(_examinationService.GetExaminationById(id));
                return Ok(examinationDTO);
            }
            catch (NotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// /adding new examination to database
        /// </summary>
        /// <param name="examinationDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 201(Created), if validation isn't successful return 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public IActionResult AddExamination(ScheduleExaminationDTO examinationDTO)
        {
            var client = new RestClient(_serviceSettings.ScheduleServiceUrl);
            var request = new RestRequest("/api/examination", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(examinationDTO);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

    }
}
