using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Constants;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {      
        [AllowAnonymous]
        [HttpGet("country/{countryId}")]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/city/country/" + countryId);
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        } 
    }
}
