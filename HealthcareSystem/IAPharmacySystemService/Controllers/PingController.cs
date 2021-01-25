using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersPharmacySystemService.Controllers
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
