using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegrationAdapters.Dtos;
using Backend.Service.Pharmacies;
using Backend.Model.Pharmacies;
using Backend.Repository;

namespace IntegrationAdapters.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
