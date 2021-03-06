﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserService.Mapper;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IGeographicalService _geographicalService;

        public CityController(IGeographicalService geographicalService)
        {
            _geographicalService = geographicalService;
        }

        [HttpGet("country/{countryId}")]
        public IActionResult GetByCountry(int countryId)
        {
            var cities = _geographicalService.GetCitiesByCountry(countryId).Select(c => c.ToCityDTO());
            return Ok(cities);
        }
    }
}