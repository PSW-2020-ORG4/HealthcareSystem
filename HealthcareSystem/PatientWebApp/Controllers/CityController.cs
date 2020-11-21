using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            _cityService.GetCities().ForEach(city => cityDTOs.Add(CityMapper.CityToCityDTO(city)));
            return Ok(cityDTOs);
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            try
            {
                _cityService.GetCitiesByCountryId(countryId).ForEach(city => cityDTOs.Add(CityMapper.CityToCityDTO(city)));
                return Ok(cityDTOs);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
