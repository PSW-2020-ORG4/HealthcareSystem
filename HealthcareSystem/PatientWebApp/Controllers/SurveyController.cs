using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public SurveyController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public ActionResult AddSurvey(SurveyDTO surveyDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithBody(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/patient/" + patientJmbg + "/permission/" + surveyDTO.ExaminationId, surveyDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("permission")]
        public ActionResult GetPermissions()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/patient/" + patientJmbg + "/permission", Method.GET);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutMedicalStaff")]
        public IActionResult GetSurveyResultAboutMedicalStaff()
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/staff", Method.GET);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutDoctor/{jmbg}")]
        public IActionResult GetSurveyResultAboutDoctor(string jmbg)
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/doctor/" + jmbg, Method.GET);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutHospital")]
        public IActionResult GetSurveyResultAboutHospital()
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/hospital", Method.GET);
        }
    }
}
