using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}