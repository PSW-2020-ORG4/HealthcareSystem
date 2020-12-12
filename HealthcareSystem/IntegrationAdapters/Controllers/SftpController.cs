using Backend.Communication.SftpCommunicator;
using Backend.Model.Pharmacies;
using Backend.Service.DrugConsumptionService;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace IntegrationAdapters.Controllers
{
    public class SftpController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IDrugConsumptionService _drugConsumptionService;
        private readonly IPharmacyService _pharmacyService;

        public SftpController(IAdapterContext adapterContext, IDrugConsumptionService drugConsumptionService, IPharmacyService pharmacyService)
        {
            _adapterContext = adapterContext;
            _drugConsumptionService = drugConsumptionService;
            _pharmacyService = pharmacyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateRange dateRange)
        {
            _adapterContext.SetPharmacySystemAdapter(_pharmacyService.GetPharmacyById(1));
            if (_adapterContext.PharmacySystemAdapter.SendDrugConsumptionRepor("fasdf"))
            {
                TempData["Success"] = "Report successfully created and uploaded!";
                Console.WriteLine("sent");
            }
            else
            {
                TempData["Unsuccess"] = "Report unsuccessfully created and uploaded!";
            }
            return RedirectToAction("Index");
        }

    }
}
