using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{jmbg}/block")]
        public IActionResult Block(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{jmbg}/activate")]
        public IActionResult Activate(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("malicious")]
        public IActionResult GetMalicious()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{jmbg}")]
        public IActionResult GetByJmbg(string jmbg)
        {
            throw new NotImplementedException();
        }
    }
}