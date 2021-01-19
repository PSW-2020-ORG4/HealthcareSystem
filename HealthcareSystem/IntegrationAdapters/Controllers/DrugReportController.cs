using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WebPush;
using IntegrationAdapters.Services;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using System.Threading.Tasks;

namespace IntegrationAdapters.Controllers
{
    public class DrugReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IDrugService _drugService;
        private readonly IPharmacySystemService _pharmacySystemService;
        private readonly IPushNotificationService _pushNotificationService;

        public DrugReportController(IAdapterContext adapterContext, 
                                    IDrugService drugService, 
                                    IPharmacySystemService pharmacySystemService,
                                    IPushNotificationService pushNotificationService)
        {
            _adapterContext = adapterContext;
            _drugService = drugService;
            _pharmacySystemService = pharmacySystemService;
            _pushNotificationService = pushNotificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateRangeDTO dateRangeDto)
        {
            DateRange dateRange = new DateRange(dateRangeDto.From, dateRangeDto.To);
            PushSubscription pushSubscription = new PushSubscription() { Endpoint = Request.Form["PushEndpoint"], P256DH = Request.Form["PushP256DH"], Auth = Request.Form["PushAuth"] };
            PushPayload pushPayload = new PushPayload();

            var reports = await _drugService.GetDrugConsuption(dateRange);
            if(reports == null)
            {
                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Comunication error!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);

                return RedirectToAction("Index");
            }
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
                System.IO.File.WriteAllText(reportFilePath + "/" + reportFileName, json);
            }
            catch(Exception dnfe)
            {
                Console.WriteLine(dnfe);

                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Error occured while creating report file!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);

                return RedirectToAction("Index");
            }
            var pharmacySystems = await _pharmacySystemService.GetAll();
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
