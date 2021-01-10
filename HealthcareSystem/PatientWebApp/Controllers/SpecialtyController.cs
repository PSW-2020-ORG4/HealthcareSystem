using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public SpecialtyController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [HttpGet]
        public IActionResult GetSpecialities()
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.UserServiceUrl, "/api/specialty", Method.GET);
        }
    }
}
