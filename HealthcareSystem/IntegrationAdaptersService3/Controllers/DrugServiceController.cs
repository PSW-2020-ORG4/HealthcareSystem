using Backend.Model.Pharmacies;
using Backend.Service.DrugAndTherapy;
using Backend.Service.DrugConsumptionService;

using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersService3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DrugServiceController : ControllerBase
    {
        private readonly IDrugService _drugService;
        private readonly IDrugConsumptionService _drugConsuptionService;

        public DrugServiceController(IDrugService drugService, IDrugConsumptionService drugConsumptionService)
        {
            _drugService = drugService;
            _drugConsuptionService = drugConsumptionService;
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAll()
        {
            return Ok(_drugService.ViewConfirmedDrugs());
        }

        [HttpPatch]
        [Route("addquantity")]
        public IActionResult AddQuantity([FromBody] AddDrugQuantityRequest request)
        {
            _drugService.AddDrugQuantity(request.Code, request.Quantity);
            return Ok();
        }

        [HttpGet]
        [Route("get/drugconsuption")]
        public IActionResult GetDrugConsuption([FromBody] DateRange request)
        {
            return Ok(_drugConsuptionService.GetDrugConsumptionForDate(request));
        }
    }
}
