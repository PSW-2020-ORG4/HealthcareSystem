using System;
using System.Linq;
using Backend.Model.Pharmacies;
using Backend.Service;
using Backend.Service.Pharmacies;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IActionBenefitSubscriptionService _actionBenefitMessageingService;

        public PharmacyController(IPharmacyService iPharmacyService, IActionBenefitSubscriptionService actionBenefitSubscriptionService)
        {
            _pharmacyService = iPharmacyService;
            _actionBenefitMessageingService = actionBenefitSubscriptionService;
        }

        public IActionResult ApiRegister()
        {
            return View(new PharmacySystem());
        }

        [HttpPost]
        public IActionResult ApiRegister(PharmacySystem pharmacy)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pharmacy.ActionsBenefitsExchangeName != null)
                pharmacy.ActionsBenefitsSubscribed = true;

            try
            {
                _pharmacyService.CreatePharmacy(pharmacy);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }

            if (pharmacy.ActionsBenefitsSubscribed)
                _actionBenefitMessageingService.Subscribe(pharmacy.ActionsBenefitsExchangeName);

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("ApiRegister");
        }

        public IActionResult Index()
        {
            return View(_pharmacyService.GetAllPharmacies().ToList());
        }

        public IActionResult Edit(int id)
        {
            var pharmacy = _pharmacyService.GetPharmacyById(id);

            if (pharmacy == null)
                return NotFound("Pharmacy does not exist.");

            return View(pharmacy);
        }

        [HttpPost]
        public IActionResult Edit(PharmacySystem pharmacy)
        {
            PharmacySystem pharmacyOld = _pharmacyService.GetPharmacyByIdNoTracking(pharmacy.Id);
            if (pharmacyOld == null)
                throw new ArgumentNullException("There is no pharmacy with id=" + pharmacy.Id);

            if (pharmacy.ActionsBenefitsExchangeName == null)
                pharmacy.ActionsBenefitsSubscribed = false;

            _pharmacyService.UpdatePharmacy(pharmacy);

            _actionBenefitMessageingService.SubscriptionEdit(pharmacyOld.ActionsBenefitsExchangeName, 
                                                             pharmacyOld.ActionsBenefitsSubscribed, 
                                                             pharmacy.ActionsBenefitsExchangeName, 
                                                             pharmacy.ActionsBenefitsSubscribed);

            return RedirectToAction("Index");
        }
    }
}