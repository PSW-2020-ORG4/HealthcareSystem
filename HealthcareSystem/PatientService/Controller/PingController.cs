using Microsoft.AspNetCore.Mvc;

namespace PatientService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Successfully pinged.");
        }
    }
}