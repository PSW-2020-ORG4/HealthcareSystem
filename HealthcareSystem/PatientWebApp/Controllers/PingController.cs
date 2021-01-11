using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Successfully pinged.");
        }
    }
}