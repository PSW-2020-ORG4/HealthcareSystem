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
        private readonly IExaminationService _examinationService;

        public SurveyController(ISurveyService surveyService, IExaminationService examinationService)
        {
            _surveyService = surveyService;
            _examinationService = examinationService;
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public ActionResult AddSurvey(SurveyDTO surveyDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/survey/patient/" + patientJmbg + "/permission/" + surveyDTO.ExaminationId, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(surveyDTO);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;
            return contentResult;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutMedicalStaff")]
        public IActionResult GetSurveyResultAboutMedicalStaff()
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/survey/report/staff");
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.StatusCode = (int)response.StatusCode;
            return contentResult;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutDoctor/{jmbg}")]
        public IActionResult GetSurveyResultAboutDoctor(string jmbg)
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/survey/report/doctor/" + jmbg);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.StatusCode = (int)response.StatusCode;
            return contentResult;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutHospital")]
        public IActionResult GetSurveyResultAboutHospital()
        {
            var client = new RestClient("http://localhost:56701");
            var request = new RestRequest("/api/survey/report/hospital");
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.StatusCode = (int)response.StatusCode;
            return contentResult;
        }
    }
}
