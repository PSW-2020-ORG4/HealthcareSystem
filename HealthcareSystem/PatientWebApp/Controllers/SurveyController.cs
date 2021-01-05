using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Validators;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly SurveyValidator _surveyValidator;

        private readonly IExaminationService _examinationService;
        private readonly ExaminationValidator _examinationValidator;


        public SurveyController(ISurveyService surveyService, IExaminationService examinationService)
        {
            _surveyService = surveyService;
            _surveyValidator = new SurveyValidator(surveyService);
            _examinationService = examinationService;
            _examinationValidator = new ExaminationValidator(_examinationService);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public ActionResult AddSurvey(SurveyDTO surveyDTO)
        {
            try
            {
                _surveyValidator.ValidateSurveyFields(surveyDTO);
                _examinationValidator.CheckIfExaminationIsFinished(surveyDTO.ExaminationId);
                _examinationValidator.CheckIfSurveyAboutExaminationIsCompleted(surveyDTO.ExaminationId);
                if (!_examinationService.GetExaminationById(surveyDTO.ExaminationId).PatientCard.PatientJmbg.Equals(HttpContext.User.FindFirst("Jmbg").Value))
                    return BadRequest("Patient can only fill out the survey for their own examinations.");
                _surveyService.AddSurvey(SurveyMapper.SurveyDTOToSurvey(surveyDTO));
                _examinationService.CompleteSurveyAboutExamination(surveyDTO.ExaminationId);
                return Ok();
            }         
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutMedicalStaff")]
        public IActionResult GetSurveyResultAboutMedicalStaff()
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/report/staff");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutDoctor/{jmbg}")]
        public IActionResult GetSurveyResultAboutDoctor(string jmbg)
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/report/doctor/" + jmbg);
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutHospital")]
        public IActionResult GetSurveyResultAboutHospital()
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/report/hospital");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

    }
}
