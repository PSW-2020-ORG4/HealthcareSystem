using Backend.Service.DrugConsumptionService;
using Backend.Service.SftpService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Backend.Model.Pharmacies;

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
            var reports = _drugConsumptionService.GetDrugConsumptionForDate(dateRange.From, dateRange.To);
            var json = JsonConvert.SerializeObject(reports);

            string reportFileName = $"{dateRange.From:yyyy-M-dd}-to-{dateRange.To:yyyy-M-dd}-report.json";

            System.IO.File.WriteAllText(reportFileName, json);
            _sftpService.UploadFile(reportFileName, "/PSW-uploads/" + reportFileName);

            return RedirectToAction("Index");
        }

        public IActionResult Download()
        {
            var localFilePath = @"E:\TinySFTP\data\PSW-downloads\test-download.txt";
            _sftpService.DownloadFile("/PSW-uploads/test-upload.txt", localFilePath);

            return RedirectToAction("Index");
        }
    }
}
