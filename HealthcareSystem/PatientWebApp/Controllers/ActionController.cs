using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
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
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.UserServiceUrl, "/api/action", Method.GET);
        }
    }
}
