using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.CustomException;
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
            return Ok(_patientService.GetAll());
        }

        [HttpGet("malicious")]
        public IActionResult GetMalicious()
        {
            return Ok(_patientService.GetMalicious());
        }

        [HttpGet("{jmbg}")]
        public IActionResult GetByJmbg(string jmbg)
        {
            return Ok(_patientService.GetByJmbg(jmbg));
        }
    }
}