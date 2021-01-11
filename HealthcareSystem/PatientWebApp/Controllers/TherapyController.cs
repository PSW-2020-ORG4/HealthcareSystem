using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientService.DTO;
using PatientWebApp.Auth;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TherapyController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public TherapyController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        /// <summary>
        /// /getting therapies linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public ActionResult GetTherapiesByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.PatientServiceUrl);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/therapy");
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// <summary>
        /// /getting searched therapies linked to a certain patient
        /// </summary>
        /// <param name="therapySearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("advance-search")]
        public ActionResult AdvanceSearchTherapies(TherapySearchDTO therapySearchDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.PatientServiceUrl);
            var request = new RestRequest("/api/patient/" + patientJmbg + "/therapy/search", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(therapySearchDTO);
            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;

        }
    }
}
