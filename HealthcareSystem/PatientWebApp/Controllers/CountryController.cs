using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Constants;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCountries()
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/country");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
