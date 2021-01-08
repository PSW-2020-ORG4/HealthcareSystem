using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using Backend.Service.DrugAndTherapy;
using Backend.Service.Tendering;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;
using Model.Manager;

namespace IntegrationAdapters.Controllers
{
    public class TenderController : Controller
    {
        private readonly ITenderService _tenderService;
        private readonly IDrugService _drugService;

        public TenderController(ITenderService tenderService, IDrugService drugService)
        {
            _tenderService = tenderService;
            _drugService = drugService;
        }
        public IActionResult Index()
        {
            return View(_tenderService.GetAllTenders().ToList());
        }

        public IActionResult NewTender()
        {
            return View(new NewTenderView()
            {
                TenderName = "",
                EndDate = DateTime.Now.AddDays(1),
                Drugs = _drugService.ViewConfirmedDrugs()
            });

        }

        public IActionResult Drugs(int tenderId)
        {
            return View(_tenderService.GetDrugsForTender(tenderId));
        }

        public JsonResult PushDrugList(List<TenderDrugDTO> tenderDrugs)
        {
            //if (ModelState.IsValid)
            //{
            //    Console.WriteLine("pooooooooooooooooop");
            //}

            foreach (var drug in tenderDrugs)
            {
                Console.WriteLine(drug);
            }     

            var result = new { Success = "True", Message = "X gon give it to ya" };
            return Json(result);
        }



        //[HttpPost]
        //public IActionResult AddDrug(NewTenderView tender, Drug drug, int quantity)
        //{
        //    tender.AddedDrugs.Add(new TenderDrugDTO()
        //    {
        //        Id = drug.Id,
        //        Name = drug.Name,
        //        Quantity = quantity
        //    });

        //    return PartialView("AddedDrugs", tender.AddedDrugs);
        //}
    }
}
