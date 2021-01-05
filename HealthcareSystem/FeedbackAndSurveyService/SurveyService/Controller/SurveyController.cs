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
            _surveyService.RecordResponse(jmbg, id, response);
            return NoContent();
        }

        [HttpGet("patient/{jmbg}/permission")]
        public IActionResult GetPermission(string jmbg)
        {
            return Ok(_surveyService.GetPermissions(jmbg));
        }

        [HttpGet("report/doctor/{jmbg}")]
        public IActionResult GetDoctorSurveyReport(string jmbg)
        {
            return Ok(_surveyService.GetDoctorSurveyReport(jmbg));
        }

        [HttpGet("report/staff")]
        public IActionResult GetMedicalStaffSurveyReport()
        {
            return Ok(_surveyService.GetMedicalStaffSurveyReport());
        }

        [HttpGet("report/hospital")]
        public IActionResult GetHospitalSurveyReport()
        {
            return Ok(_surveyService.GetHospitalSurveyReport());
        }
    }
}