using Backend.Model.Pharmacies;
using Backend.Service.DrugConsumptionService;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WebPush;
using IntegrationAdapters.Services;
using IntegrationAdapters.Dtos;
using System.IO;

namespace IntegrationAdapters.Controllers
{
    public class DrugReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IDrugConsumptionService _drugConsumptionService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IPushNotificationService _pushNotificationService;

        public DrugReportController(IAdapterContext adapterContext, 
                                    IDrugConsumptionService drugConsumptionService, 
                                    IPharmacyService pharmacyService,
                                    IPushNotificationService pushNotificationService)
        {
            _adapterContext = adapterContext;
            _drugConsumptionService = drugConsumptionService;
            _pharmacyService = pharmacyService;
            _pushNotificationService = pushNotificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateRange dateRange)
        {
            PushSubscription pushSubscription = new PushSubscription() { Endpoint = Request.Form["PushEndpoint"], P256DH = Request.Form["PushP256DH"], Auth = Request.Form["PushAuth"] };
            PushPayload pushPayload = new PushPayload();

            var reports = _drugConsumptionService.GetDrugConsumptionForDate(dateRange);
            if(reports.Count == 0)
            {
                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Nothing found in given data range!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);

                return RedirectToAction("Index");
            }
            var json = JsonConvert.SerializeObject(reports, Formatting.Indented);
            var reportFileName = $"{dateRange.From:yyyy-MM-dd}-to-{dateRange.To:yyyy-MM-dd}-report.json";
            var reportFilePath = "Resources";
            try
            {
                System.IO.File.WriteAllText(Path.Combine(reportFilePath, reportFileName), json);
            }
            catch(Exception dnfe)
            {
                Console.WriteLine(dnfe);

                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Error occured while creating report file!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);

                return RedirectToAction("Index");
            }
            var pharmacySystems = _pharmacyService.GetAllPharmacies();
            foreach (var ps in pharmacySystems)
            {
                if (_adapterContext.SetPharmacySystemAdapter(ps) == null)
                    continue;

                if (_adapterContext.PharmacySystemAdapter.SendDrugConsumptionReport(reportFilePath, reportFileName))
                {
                    pushPayload.Title = "Success";
                    pushPayload.Message = "Report successfully created and uploaded to " + ps.Name + "!";
                }
                else
                {
                    pushPayload.Title = "Unsuccess";
                    pushPayload.Message = "Report upload to " + ps.Name + " unsuccessfull!";
                }
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);
            }

            return RedirectToAction("Index");
        }

    }
}
