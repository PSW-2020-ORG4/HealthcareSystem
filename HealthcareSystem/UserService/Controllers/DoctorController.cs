﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserService.Mapper;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("specialty/{specialtyId}")]
        public IActionResult GetBySpecialty(int specialtyId)
        {
            var doctors = _doctorService.GetBySpecialty(specialtyId).Select(d => d.ToDoctorDTO());
            return Ok(doctors);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAll().Select(d => d.ToDoctorDTO());
            return Ok(doctors);
        }
    }
}