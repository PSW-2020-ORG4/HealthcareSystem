using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public IActionResult Ping()
        {
            return Ok("Service-1");
        }
    }
}
