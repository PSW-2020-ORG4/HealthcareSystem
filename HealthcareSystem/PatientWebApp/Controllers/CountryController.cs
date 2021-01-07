using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ServiceSettings _serviceSettings;

        public CountryController(ICountryService countryService, IOptions<ServiceSettings> serviceSettings)
        {
            _countryService = countryService;
            _serviceSettings = serviceSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCountries()
        {
            List<CountryDTO> countryDTOs = new List<CountryDTO>();
            try
            {
                _countryService.GetCountries().ForEach(country => countryDTOs.Add(CountryMapper.CountryToCountryDTO(country)));
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            return Ok(countryDTOs);
        }
    }
}
