using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyReportService _reportService;
        private readonly ISurveyResponseService _responseService;

        public SurveyController(ISurveyReportService reportService, ISurveyResponseService responseService)
        {
            _reportService = reportService;
            _responseService = responseService;
        }

        [HttpPost("patient/{jmbg}/permission/{id}")]
        public IActionResult RespondToSurvey(string jmbg, int id, SurveyResponseDTO response)
        {
            _responseService.RecordResponse(jmbg, id, response);
            return NoContent();
        }

        [HttpGet("patient/{jmbg}/permission")]
        public IActionResult GetPermission(string jmbg)
        {
            var permissions = _responseService.GetPermissions(jmbg).Select(
                p => new SurveyPermissionDTO(p.Id, p.DoctorJmbg.Value));
            return Ok(permissions);
        }

        [HttpGet("report/doctor/{jmbg}")]
        public IActionResult GetDoctorSurveyReport(string jmbg)
        {
            return Ok(_reportService.GetDoctorSurveyReport(jmbg));
        }

        [HttpGet("report/staff")]
        public IActionResult GetMedicalStaffSurveyReport()
        {
            return Ok(_reportService.GetMedicalStaffSurveyReport());
        }

        [HttpGet("report/hospital")]
        public IActionResult GetHospitalSurveyReport()
        {
            return Ok(_reportService.GetHospitalSurveyReport());
        }
    }
}