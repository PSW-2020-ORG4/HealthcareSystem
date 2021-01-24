using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersDrugService.Controllers
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
