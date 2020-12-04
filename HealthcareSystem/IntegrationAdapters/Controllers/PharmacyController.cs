﻿using System;
using System.Linq;
using Backend.Model.Pharmacies;
using Backend.Service;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IActionBenefitMessageingService _actionBenefitMessageingService;

        public PharmacyController(IPharmacyService iPharmacyService, RabbitMqActionBenefitMessageingService actionBenefitMessageingService)
        {
            _pharmacyService = iPharmacyService;
            _actionBenefitMessageingService = actionBenefitMessageingService;
        }

        public IActionResult ApiRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApiRegister(Pharmacy pharmacy)
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
        public IActionResult Edit(Pharmacy pharmacy)
        {
            Pharmacy pharmacyOld = _pharmacyService.GetPharmacyByIdNoTracking(pharmacy.Id);
            if (pharmacy == null)
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