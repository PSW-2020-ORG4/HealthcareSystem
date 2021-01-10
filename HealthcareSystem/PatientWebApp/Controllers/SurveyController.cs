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

            var client = new RestClient(_serviceSettings.FeedbackAndSurveyServiceUrl);
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

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("permission")]
        public ActionResult GetPermissions()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            return RequestAdapter.SendGetRequest(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/patient/" + patientJmbg + "/permission");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutMedicalStaff")]
        public IActionResult GetSurveyResultAboutMedicalStaff()
        {
            return RequestAdapter.SendGetRequest(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/staff");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutDoctor/{jmbg}")]
        public IActionResult GetSurveyResultAboutDoctor(string jmbg)
        {
            return RequestAdapter.SendGetRequest(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/doctor/" + jmbg);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("surveyResultAboutHospital")]
        public IActionResult GetSurveyResultAboutHospital()
        {
            return RequestAdapter.SendGetRequest(_serviceSettings.FeedbackAndSurveyServiceUrl, "/api/survey/report/hospital");
        }
    }
}
