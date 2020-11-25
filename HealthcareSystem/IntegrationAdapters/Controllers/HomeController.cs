using System.Diagnostics;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IntegrationAdapters.Dtos;
using Backend.Service.Pharmacies;
using Backend.Model.Pharmacies;
using Backend.Repository;

namespace IntegrationAdapters.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PharmacyService _pharmacyService;
        
        public HomeController(ILogger<HomeController> logger, MyDbContext ctx)
        {
            _logger = logger;
            _pharmacyService = new PharmacyService(new MySqlPharmacyRepo(ctx));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ApiRegister()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
