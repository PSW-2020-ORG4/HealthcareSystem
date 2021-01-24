using IntegrationAdaptersService2.DTO;
using IntegrationAdaptersService2.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActionBenefitServiceController : ControllerBase
    {
        private readonly IActionBenefitService _actionBenefitService;
        private readonly IRabbitMqActionBenefitService _rabbitMqActionBenefitService;

        public ActionBenefitServiceController(IActionBenefitService actionBenefitService, IRabbitMqActionBenefitService rabbitMqActionBenefitService)
        {
            _actionBenefitService = actionBenefitService;
            _rabbitMqActionBenefitService = rabbitMqActionBenefitService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get([FromBody] int id)
        {
            return Ok(_actionBenefitService.GetActionBenefitById(id));
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAll()
        {
            return Ok(_actionBenefitService.GetAllActionsBenefits());
        }

        [HttpPatch]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody] string exchangeName)
        {
            _rabbitMqActionBenefitService.Subscribe(exchangeName);
            return Ok();
        }

        [HttpPatch]
        [Route("setpublic")]
        public IActionResult SetPublic([FromBody] SetPublicRequest setPublicRequest)
        {
            _actionBenefitService.MakePublic(setPublicRequest.Id, setPublicRequest.IsPublic);
            return Ok();
        }

        [HttpPatch]
        [Route("subscriptionedit")]
        public IActionResult SubscriptionEdit([FromBody] SubscritpionEditRequest request)
        {
            _rabbitMqActionBenefitService.SubscriptionEdit(request.ExOld, request.SubOld, request.ExNew, request.SubNew);
            return Ok();
        }
    }
}
