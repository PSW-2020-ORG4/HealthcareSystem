using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/city/country/" + countryId);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        } 
    }
}
