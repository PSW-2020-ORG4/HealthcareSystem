using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public IActionResult Ping()
        {
            return Ok("Service-4");
        }
    }
}
