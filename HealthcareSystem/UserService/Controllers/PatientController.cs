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
            try
            {
                _patientService.Block(jmbg);
                return NoContent();
            }
            catch (DataStorageException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ExternalConnectionException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost("{jmbg}/activate")]
        public IActionResult Activate(string jmbg)
        {
            try
            {
                _patientService.Activate(jmbg);
                return NoContent();
            }
            catch (DataStorageException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ExternalConnectionException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_patientService.GetAll());
            }
            catch (DataStorageException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ExternalConnectionException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("malicious")]
        public IActionResult GetMalicious()
        {
            try
            {
                return Ok(_patientService.GetMalicious());
            }
            catch (DataStorageException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ExternalConnectionException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{jmbg}")]
        public IActionResult GetByJmbg(string jmbg)
        {
            try
            {
                return Ok(_patientService.GetByJmbg(jmbg));
            }
            catch (DataStorageException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (ExternalConnectionException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}