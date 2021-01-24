using Backend.Model.Pharmacies;
using IntegrationAdaptersPharmacySystemService.MicroserviceComunicator;
using IntegrationAdaptersPharmacySystemService.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntegrationAdaptersPharmacySystemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PharmacySystemServiceController : ControllerBase
    {
        private readonly IPharmacyService _pharmacySystemService;
        private readonly IActionBenefitService _actionBenefitService;

        public PharmacySystemServiceController(IPharmacyService pharmacySystemService, IActionBenefitService actionBenefitService)
        {
            _pharmacySystemService = pharmacySystemService;
            _actionBenefitService = actionBenefitService;
        }

        [HttpGet]
        [Route("get", Name = "get")]
        public IActionResult Get([FromBody] int id)
        {
            return Ok(_pharmacySystemService.GetPharmacyById(id));
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAll()
        {
            return Ok(_pharmacySystemService.GetAllPharmacies());
        }

        [HttpGet]
        [Route("get/subscribed")]
        public IActionResult GetBySubscribed([FromBody] bool subscribed)
        {
            return Ok(_pharmacySystemService.GetPharmaciesBySubscribed(subscribed));
        }

        [HttpPost]
        public IActionResult Create([FromBody] PharmacySystem pharmacySystem)
        {
            if (pharmacySystem.isValid())
            {
                _pharmacySystemService.CreatePharmacy(pharmacySystem);
                if (pharmacySystem.ActionsBenefitsSubscribed)
                    _actionBenefitService.Subscribe(pharmacySystem.ActionsBenefitsExchangeName);
                return CreatedAtRoute("get", pharmacySystem);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PharmacySystem pharmacySystem)
        {
            if(pharmacySystem.isValid())
            {
                var pharmacyOld = _pharmacySystemService.GetPharmacyByIdNoTracking(pharmacySystem.Id);
                if (pharmacyOld == null)
                    throw new ArgumentNullException("There is no pharmacy with id=" + pharmacySystem.Id);
                if (pharmacySystem.ActionsBenefitsExchangeName == null)
                    pharmacySystem.ActionsBenefitsSubscribed = false;
                if(await _actionBenefitService.SubscriptionEdit(pharmacyOld.ActionsBenefitsExchangeName,
                                                           pharmacyOld.ActionsBenefitsSubscribed,
                                                           pharmacySystem.ActionsBenefitsExchangeName,
                                                           pharmacySystem.ActionsBenefitsSubscribed))
                {
                    _pharmacySystemService.UpdatePharmacy(pharmacySystem);
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
