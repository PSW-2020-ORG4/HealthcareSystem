using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PrescriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
