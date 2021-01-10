using System;
using System.Linq;
using Backend.Service.DrugAndTherapy;
using Backend.Service.Tendering;
using IntegrationAdapters.Dtos;
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
        public IActionResult Index()
        {
            return View(_tenderService.GetAllTenders().ToList());
        }

        public IActionResult NewTender()
        {
            NewTenderView tender = new NewTenderView()
            {
                Drugs = _drugService.ViewConfirmedDrugs()
            };

            return View(tender);
        }

        public IActionResult Drugs(int tenderId)
        {
            return View(_tenderService.GetDrugsForTender(tenderId));
        }

        public IActionResult Offers(int tenderId)
        {
            return View(_tenderService.GetMessagesForTender(tenderId));
        }

        public IActionResult Offer(int messageId)
        {
            return View(_tenderService.GetOffersForMessage(messageId));
        }

        public IActionResult AcceptOffer(int messageId)
        {
            var tender = _tenderService.GetTenderByMessageId(messageId);
            tender.IsClosed = true;
            tender.WinningMessage = messageId;
            _tenderService.UpdateTender(tender);
            return RedirectToAction("Index");
        }

        public IActionResult DeclineOffer(int messageId)
        {
            var tender = _tenderService.GetTenderByMessageId(messageId);
            _tenderService.DeclineMessage(messageId);
            return RedirectToAction("Offers", new { tenderId = tender.Id });
        }

    }
}
