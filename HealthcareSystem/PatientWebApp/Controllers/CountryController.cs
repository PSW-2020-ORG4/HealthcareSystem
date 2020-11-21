using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            List<CountryDTO> countryDTOs = new List<CountryDTO>();
            _countryService.GetCountries().ForEach(country => countryDTOs.Add(CountryMapper.CountryToCountryDTO(country)));
            return Ok(countryDTOs);
        }
    }
}
