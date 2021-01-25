using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _tenderService.GetAllTenders());
        }

        public async Task<IActionResult> NewTender()
        {
            NewTenderView tender = new NewTenderView()
            {
                Drugs = await _drugService.GetAll(),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                
            };

            return View(tender);
        }

        public async Task<IActionResult> DrugsAsync(int tenderId)
        {
            return View(await _tenderService.GetDrugsByTender(tenderId));
        }

        public async Task<IActionResult> OffersAsync(int tenderId)
        {
            Tender tender = await _tenderService.GetTender(tenderId);
            if (tender == null)
                return RedirectToAction("Index");

            if(tender.IsClosed)
            {
                TenderMessage tenderMessage = await _tenderService.GetAcceptedMessage(tenderId);
                if (tenderMessage == null)
                    return RedirectToAction("Index");
                return RedirectToAction("Offer", new { messageId = tenderMessage.Id });
            }

            return View(await _tenderService.GetAllMessages(tenderId));
        }

        public async Task<IActionResult> OfferAsync(int messageId)
        {
            return View(await _tenderService.GetMessage(messageId));
        }

        public async Task<IActionResult> AcceptOfferAsync(int messageId)
        {
            var tender = await _tenderService.GetTenderByMessage(messageId);
            if (tender.IsClosed)
                return RedirectToAction("Offers", new { tenderId = tender.Id });

            await _tenderService.AcceptMessage(messageId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CloseTenderAsync(int tenderId)
        {
            await _tenderService.CloseTender(tenderId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PushDrugListAsync([FromBody]NewTenderView tenderDto)
        {
            if (!tenderDto.IsValid())
                return StatusCode(400);

            Tender tender = new Tender()
            {
                Name = tenderDto.TenderName,
                IsClosed = false,
                EndDate = tenderDto.EndDate,
                Drugs = new List<TenderDrug>()
            };
            foreach(var drug in tenderDto.AddedDrugs)
            {
                tender.Drugs.Add(new TenderDrug() {DrugId = drug.Id, Quantity = drug.Quantity});
            }
            await _tenderService.CreateTender(tender);

            return StatusCode(204);
        }
    }
}
