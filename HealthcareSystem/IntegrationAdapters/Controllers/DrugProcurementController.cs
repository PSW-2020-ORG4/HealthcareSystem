﻿using System;
using Backend.Model.Exceptions;
using Backend.Service.DrugAndTherapy;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class DrugProcurementController : Controller
    {
        private readonly IDrugService _drugService;
        private readonly IAdapterContext _adapterContext;
        private readonly IPharmacyService _pharmacyService;
        public DrugProcurementController(IDrugService drugService, IAdapterContext adapterContext, IPharmacyService pharmacyService)
        {
            _drugService = drugService;
            _adapterContext = adapterContext;
            _pharmacyService = pharmacyService;
        }

        public IActionResult Index(DrugProcurementDTO drugProcurementDto)
        {
            return View(drugProcurementDto);
        }

        public IActionResult Order(DrugProcurementDTO drugProcurementDto)
        {
            var pharmacySystem = _pharmacyService.GetPharmacyById(drugProcurementDto.PharmacySystemId);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
                throw new ArgumentNullException("There is no pharmacy with id=" + drugProcurementDto.PharmacySystemId);

            if (_adapterContext.PharmacySystemAdapter.OrderDrugs(drugProcurementDto.PharmacyId,
                drugProcurementDto.DrugId, drugProcurementDto.Quantity))
            {
                try
                {
                    _drugService.AddDrugQuantity(drugProcurementDto.Code, drugProcurementDto.Quantity);
                    TempData["Success"] = "Order successful!";
                }
                catch (NotFoundException ex)
                {
                    TempData["Failure"] = "Drug procured but does not exsist in our database, contact administrator!";
                }
            }
            else
            {
                TempData["Failure"] = "Order unsuccessful!";
            }

            return RedirectToAction("Index", drugProcurementDto);
        }
    }
}