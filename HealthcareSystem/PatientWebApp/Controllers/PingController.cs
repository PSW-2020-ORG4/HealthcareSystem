using Microsoft.AspNetCore.Authorization;
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