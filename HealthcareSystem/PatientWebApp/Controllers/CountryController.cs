using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Constants;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        [AllowAnonymous]
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
