using System;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using Backend.Service;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPharmacySystemService _pharmacySystemService;

        public PharmacyController(IPharmacySystemService pharmacySystemService)
        {
            _pharmacySystemService = pharmacySystemService;
        }

        public IActionResult ApiRegister()
        {
            return View(new PharmacySystem());
        }

        [HttpPost]
        public async Task<IActionResult> ApiRegister(PharmacySystem pharmacy)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pharmacy.ActionsBenefitsExchangeName != null)
                pharmacy.ActionsBenefitsSubscribed = true;

            try
            {
                await _pharmacySystemService.Create(pharmacy);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("ApiRegister");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pharmacySystemService.GetAll());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pharmacy = await _pharmacySystemService.Get(id);

            if (pharmacy == null)
                return NotFound("Pharmacy does not exist.");

            return View(pharmacy);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PharmacySystem pharmacy)
        {
            if(await _pharmacySystemService.Update(pharmacy))
                return RedirectToAction("Index");
            return BadRequest("Something went wrong");
        }
    }
}