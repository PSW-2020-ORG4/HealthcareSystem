using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Service;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAndSurveyService.SurveyService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
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