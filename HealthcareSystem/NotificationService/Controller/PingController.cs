using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controller
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