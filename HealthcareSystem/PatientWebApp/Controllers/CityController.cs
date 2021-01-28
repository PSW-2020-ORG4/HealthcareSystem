using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public CityController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet("country/{countryId}")]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.UserServiceUrl, "/api/city/country/" + countryId, Method.GET);
        }
    }
}
