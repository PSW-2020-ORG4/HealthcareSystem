using Backend.Model.Pharmacies;
using Backend.Service.DrugAndTherapy;
using IntegrationAdaptersDrugService.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Backend.Service.DrugAndTherapy;
using IntegrationAdaptersDrugService.Service;
using Backend.Model.Exceptions;

namespace IntegrationAdaptersDrugService.Controllers
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
            try
            {
                _drugService.AddDrugQuantity(request.Code, request.Quantity);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        [Route("get/drugconsuption")]
        public async Task<IActionResult> GetDrugConsuption()
        {
            DateRange request = null;
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                request = JsonConvert.DeserializeObject<DateRange>(await reader.ReadToEndAsync());
            }
            return Ok(_drugConsuptionService.GetDrugConsumptionForDate(request));
        }
    }
}
