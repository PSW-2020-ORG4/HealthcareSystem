using System.Linq;
using Backend.Model.Pharmacies;
using IntegrationAdaptersService4.Service;
using IntegrationAdaptersService4.Service.RabbitMqTenderingService;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenderServiceController : ControllerBase
    {
        private readonly ITenderService _tenderService;
        private readonly ITenderMessageService _tenderMessageService;
        private readonly IRabbitMqTenderingService _rabbitMqTenderingService;
        public TenderServiceController(ITenderService tenderService,
                                       ITenderMessageService tenderMessageService, 
                                       IRabbitMqTenderingService rabbitMqTenderingService)
        {
            _tenderService = tenderService;
            _tenderMessageService = tenderMessageService;
            _rabbitMqTenderingService = rabbitMqTenderingService;
        }

        [HttpGet]
        [Route("tender/get", Name = "tender/get")]
        public IActionResult GetTender([FromBody] int tenderId)
        {
            return Ok(_tenderService.GetTenderById(tenderId));
        }

        [HttpGet]
        [Route("tender/get/message")]
        public IActionResult GetTenderByMessage([FromBody] int messageId)
        {
            return Ok(_tenderService.GetTenderByMessageId(messageId));
        }

        [HttpGet]
        [Route("tender/get/all")]
        public IActionResult GetAllTenders()
        {
            return Ok(_tenderService.GetAllTenders().ToList());
        }

        [HttpPost]
        [Route("tender/create")]
        public IActionResult CreateTender([FromBody] TenderDTO tenderDto)
        {
            Tender tender = new Tender() { Name = tenderDto.Name, EndDate = tenderDto.EndDate, Drugs = tenderDto.Drugs };
            if (tender.isValid())
            {
                _tenderService.CreateTender(tender);
                _rabbitMqTenderingService.AddQueue(tender);
                return CreatedAtRoute("tender/get", tender);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("tender/close")]
        public IActionResult CloseTender([FromBody] int tenderId)
        {
            var tender = _tenderService.GetTenderById(tenderId);
            tender.IsClosed = true;
            _tenderService.UpdateTender(tender);
            _rabbitMqTenderingService.RemoveQueue(tender);
            _rabbitMqTenderingService.NotifyParticipants(tender.Id);
            return Ok();
        }

        [HttpGet]
        [Route("drug/get")]
        public IActionResult GetDrugsForTender([FromBody] int tenderId)
        {
            return Ok(_tenderService.GetDrugsForTender(tenderId));
        }

        [HttpGet]
        [Route("message/get")]
        public IActionResult GetMessage([FromBody] int messageId)
        {
            return Ok(_tenderMessageService.GetById(messageId));
        }

        [HttpGet]
        [Route("message/get/all")]
        public IActionResult GetAllMessages([FromBody] int tenderId)
        {
            return Ok(_tenderMessageService.GetAllByTender(tenderId));
        }

        [HttpGet]
        [Route("message/get/accepted")]
        public IActionResult GetAcceptedMessage([FromBody] int tenderId)
        {
            return Ok(_tenderMessageService.GetAcceptedByTenderId(tenderId));
        }

        [HttpPatch]
        [Route("message/accept")]
        public IActionResult AcceptMessage([FromBody] int messageId)
        {
            var tender = _tenderService.GetTenderByMessageId(messageId);
            var message = _tenderMessageService.GetById(messageId);
            message.IsAccepted = true;
            tender.IsClosed = true;
            _tenderMessageService.UpdateTenderMessage(message);
            _tenderService.UpdateTender(tender);
            _rabbitMqTenderingService.RemoveQueue(tender);
            _rabbitMqTenderingService.NotifyParticipants(tender.Id);
            return Ok();
        }
    }
}
