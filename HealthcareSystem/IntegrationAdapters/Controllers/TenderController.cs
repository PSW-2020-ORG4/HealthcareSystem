using System.Collections.Generic;
using System.Linq;
using Backend.Model.Pharmacies;
using Backend.Service;
using Backend.Service.DrugAndTherapy;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class TenderController : Controller
    {
        private readonly ITenderService _tenderService;
        private readonly IDrugService _drugService;
        private readonly ITenderMessageService _tenderMessageService;
        private readonly IRabbitMqTenderingService _tenderingService;

        public TenderController(ITenderService tenderService, ITenderMessageService tenderMessageService, IRabbitMqTenderingService tenderingService, IDrugService drugService)
        {
            _tenderService = tenderService;
            _drugService = drugService;
            _tenderMessageService = tenderMessageService;
            _tenderingService = tenderingService;
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
            Tender tender = _tenderService.GetTenderById(tenderId);
            if (tender == null)
                return RedirectToAction("Index");

            if(tender.IsClosed)
            {
                TenderMessage tenderMessage = _tenderMessageService.GetAcceptedByTenderId(tenderId);
                return RedirectToAction("Offer", new { messageId = tenderMessage.Id });
            }

            return View(_tenderMessageService.GetAllByTender(tenderId));
        }

        public IActionResult Offer(int messageId)
        {
            return View(_tenderMessageService.GetById(messageId));
        }

        public IActionResult AcceptOffer(int messageId)
        {
            var message = _tenderMessageService.GetById(messageId);
            message.IsAccepted = true;
            _tenderMessageService.UpdateTenderMessage(message);
            var tender = _tenderService.GetTenderByMessageId(messageId);
            tender.IsClosed = true;
            _tenderService.UpdateTender(tender);
            _tenderingService.RemoveQueue(tender);
            _tenderingService.NotifyParticipants(tender.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PushDrugList([FromBody]NewTenderView tenderDto)
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
            _tenderService.CreateTender(tender);
            _tenderingService.AddQueue(tender);

            return StatusCode(204);
        }
    }
}
