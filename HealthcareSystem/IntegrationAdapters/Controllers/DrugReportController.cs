using Backend.Communication.SftpCommunicator;
using Backend.Model.Pharmacies;
using Backend.Service.DrugConsumptionService;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace IntegrationAdapters.Controllers
{
    public class DrugReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IDrugConsumptionService _drugConsumptionService;
        private readonly IPharmacyService _pharmacyService;

        public DrugReportController(IAdapterContext adapterContext, 
                                    IDrugConsumptionService drugConsumptionService, 
                                    IPharmacyService pharmacyService)
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
            var reports = _drugConsumptionService.GetDrugConsumptionForDate(dateRange);
            if(reports.Count == 0)
            {
                TempData["Unsuccess"] = "Nothing found in given data range!";
                return RedirectToAction("Index");
            }
            var json = JsonConvert.SerializeObject(reports, Formatting.Indented);
            var reportFileName = $"{dateRange.From:yyyy-MM-dd}-to-{dateRange.To:yyyy-MM-dd}-report.json";
            var reportFilePath = "Resources";
            try
            {
                System.IO.File.WriteAllText(reportFilePath + "/" + reportFileName, json);
            }
            catch(DirectoryNotFoundException dnfe)
            {
                Console.WriteLine(dnfe);
                TempData["Unsuccess"] = "Error occured while creating report file!";
                return RedirectToAction("Index");
            }
            var pharmacySystems = _pharmacyService.GetAllPharmacies();
            foreach (var ps in pharmacySystems)
            {
                if (_adapterContext.SetPharmacySystemAdapter(ps) == null)
                    continue;

                if (_adapterContext.PharmacySystemAdapter.SendDrugConsumptionRepor(reportFilePath, reportFileName))
                {
                    TempData["Success"] = "Report successfully created and uploaded!";
                    Console.WriteLine("sent");
                }
                else
                {
                    TempData["Unsuccess"] = $"Report upload to {ps.Name} was unsuccessfull!";
                }
            }

            return RedirectToAction("Index");
        }

    }
}
