using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public IActionResult Ping()
        {
            return Ok("Service-3");
        }
    }
}
