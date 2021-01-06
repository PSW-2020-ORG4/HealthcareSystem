using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.Exceptions;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using Model.Users;
using Newtonsoft.Json;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Validators;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly ExaminationValidator _examinationValidator;
        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
            _examinationValidator = new ExaminationValidator(_examinationService);
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
            var client = new RestClient("http://localhost:" + 65428);
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
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                Examination examination= _examinationService.GetExaminationById(id);
                if (!patientJmbg.Equals(examination.PatientCard.PatientJmbg)) {
                    return StatusCode(403, "Patient tried to cancel someone else's examination");
                }
                _examinationValidator.CheckIfExaminationCanBeCanceled(id);
                _examinationService.CancelExamination(id);
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
            
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
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetCanceledExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Patient's jmbg cannot be null.");
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
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
            var client = new RestClient("http://localhost:" + 65428);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/examination");
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

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
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetFollowingExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Patient's jmbg cannot be null.");
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
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
        public IActionResult AddExamination(ExaminationDTO examinationDTO)
        {
            try
            {
                var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
                examinationDTO.PatientJmbg = patientJmbg;
                _examinationValidator.ValidateExaminationFields(examinationDTO);
                _examinationService.AddExamination(ExaminationMapper.ExaminationDTOToExamination(examinationDTO));
                return StatusCode(201);
            }
            catch(ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

    }
}
