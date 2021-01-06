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
        public IActionResult GetSpecialities()
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/specialty");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
