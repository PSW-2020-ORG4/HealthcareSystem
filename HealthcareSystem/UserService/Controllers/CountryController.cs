using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IGeographicalService _geographicalService;

        public CountryController(IGeographicalService geographicalService)
        {
            _geographicalService = geographicalService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var countries = _geographicalService.GetAllCountries().Select(c => c.GetMemento());
            return Ok(countries);
        }
    }
}