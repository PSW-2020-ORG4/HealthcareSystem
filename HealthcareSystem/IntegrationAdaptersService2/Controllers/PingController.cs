using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public IActionResult Ping()
        {
            return Ok("Service-2");
        }
    }
}
