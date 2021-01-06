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
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ServiceSettings _serviceSettings;

        public CityController(ICityService cityService, IOptions<ServiceSettings> serviceSettings)
        {
            _cityService = cityService;
            _serviceSettings = serviceSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCities()
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            try
            {
                _cityService.GetCities().ForEach(city => cityDTOs.Add(CityMapper.CityToCityDTO(city)));
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            return Ok(cityDTOs);
        }

        [AllowAnonymous]
        [HttpGet("{countryId}")]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            try
            {
                _cityService.GetCitiesByCountryId(countryId).ForEach(city => cityDTOs.Add(CityMapper.CityToCityDTO(city)));
                return Ok(cityDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
