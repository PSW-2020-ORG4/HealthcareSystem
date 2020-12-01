using Backend.Model.Pharmacies;
using Backend.Service.DrugConsumptionService;
using Backend.Service.SftpService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntegrationAdapters.Controllers
{
    public class SftpController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ISftpService _sftpService;
        private readonly IDrugConsumptionService _drugConsumptionService;

        public SftpController(ISftpService sftpService, IDrugConsumptionService drugConsumptionService)
        {
            _sftpService = sftpService;
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
            _sftpService.UploadFile(reportFileName, "/PSW-uploads/" + reportFileName);

            TempData["Success"] = "Report successfully created and uploaded!";
            return RedirectToAction("Index");
        }

    }
}
