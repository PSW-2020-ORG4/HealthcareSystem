using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public ActionController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public IActionResult GetActionBenefits()
        {
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/action");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
