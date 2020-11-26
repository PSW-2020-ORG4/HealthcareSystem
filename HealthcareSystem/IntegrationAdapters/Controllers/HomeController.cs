using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegrationAdapters.Dtos;
using Backend.Service.Pharmacies;
using Backend.Model.Pharmacies;
using Backend.Repository;

namespace IntegrationAdapters.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly PharmacyService _pharmacyService;
       
        public HomeController(IPharmacyRepo iPharmacyRepo)
        {
            _pharmacyService = new PharmacyService(iPharmacyRepo);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ApiRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApiRegister(Pharmacy pharmacy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _pharmacyService.CreatePharmacy(pharmacy);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("ApiRegister");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
