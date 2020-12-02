using Backend.Communication.SftpCommunicator;
using Backend.Model.Pharmacies;
using Backend.Service.DrugConsumptionService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntegrationAdapters.Controllers
{
    public class SftpController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ISftpCommunicator _sftpCommunicator;
        private readonly IDrugConsumptionService _drugConsumptionService;

        public SftpController(ISftpCommunicator sftpCommunicator, IDrugConsumptionService drugConsumptionService)
        {
            _sftpCommunicator = sftpCommunicator;
            _drugConsumptionService = drugConsumptionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateRange dateRange)
        {
            var reports = _drugConsumptionService.GetDrugConsumptionForDate(dateRange);
            var json = JsonConvert.SerializeObject(reports);

            var reportFileName = $"{dateRange.From:yyyy-MM-dd}-to-{dateRange.To:yyyy-MM-dd}-report.json";

            System.IO.File.WriteAllText(reportFileName, json);
            _sftpCommunicator.UploadFile(reportFileName, "/PSW-uploads/" + reportFileName);

            TempData["Success"] = "Report successfully created and uploaded!";
            return RedirectToAction("Index");
        }

    }
}
