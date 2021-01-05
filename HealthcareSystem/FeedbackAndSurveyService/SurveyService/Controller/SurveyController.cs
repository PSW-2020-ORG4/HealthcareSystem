using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackAndSurveyService.SurveyService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAndSurveyService.SurveyService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly Service.SurveyService _surveyService;

        public SurveyController(Service.SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpPost("patient/{jmbg}/permission/{id}")]
        public IActionResult RespondToSurvey(string jmbg, int id, SurveyResponseDTO response)
        {
            throw new NotImplementedException();
        }

        [HttpGet("patient/{jmbg}/permission")]
        public IActionResult GetPermission(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpGet("report/doctor/{jmbg}")]
        public IActionResult GetDoctorSurveyReport(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpGet("report/staff")]
        public IActionResult GetMedicalStaffSurveyReport()
        {
            throw new NotImplementedException();
        }

        [HttpGet("report/hospital")]
        public IActionResult GetHospitalSurveyReport()
        {
            throw new NotImplementedException();
        }
    }
}