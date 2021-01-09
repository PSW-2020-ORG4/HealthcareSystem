using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.CustomException;
using UserService.Mapper;
using UserService.Model;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public IActionResult Register()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{jmbg}/block")]
        public IActionResult Block(string jmbg)
        {
            _patientService.Block(jmbg);
            return NoContent();
        }

        [HttpPost("{jmbg}/activate")]
        public IActionResult Activate(string jmbg)
        {
            _patientService.Activate(jmbg);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patients = _patientService.GetAll().Select(p => p.ToPatientDTO());
            return Ok(patients);
        }

        [HttpGet("malicious")]
        public IActionResult GetMalicious()
        {
            var maliciousPatients = _patientService.GetMalicious().Select(p => p.ToMaliciousPatientDTO());
            return Ok(maliciousPatients);
        }

        [HttpGet("{jmbg}")]
        public IActionResult GetByJmbg(string jmbg)
        {
            var patient = _patientService.Get(jmbg).ToPatientDTO();
            return Ok(patient);
        }
    }
}