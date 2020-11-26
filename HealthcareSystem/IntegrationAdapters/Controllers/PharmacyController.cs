using System;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service.Pharmacies;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly PharmacyService _pharmacyService;

        public PharmacyController(IPharmacyRepo iPharmacyRepo)
        {
            _pharmacyService = new PharmacyService(iPharmacyRepo);
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

            if (pharmacy.ActionsBenefitsExchangeName != null)
            {
                pharmacy.ActionsBenefitsSubscribed = true;
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
    }
}
