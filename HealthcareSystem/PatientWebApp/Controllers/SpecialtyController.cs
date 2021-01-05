using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Constants;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCountries()
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/specialty");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
