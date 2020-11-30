using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using Backend.Service.SftpService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class SftpController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ISftpService _sftpService;

        public SftpController(ISftpService sftpService)
        {
            _sftpService = sftpService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            var localFilePath = @"C:\Users\Stefan\Desktop\PSW\test.txt";
            _sftpService.UploadFile(localFilePath, "/PSW-uploads/test-upload.txt");

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
