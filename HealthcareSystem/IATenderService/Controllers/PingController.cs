using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersTenderService.Controllers
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
