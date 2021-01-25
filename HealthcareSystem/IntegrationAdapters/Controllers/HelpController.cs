using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelpController : ControllerBase
    {
        private readonly IPharmacySystemService _pharmacySystemService;
        private readonly IAdapterContext _adapterContext;

        public HelpController(IPharmacySystemService pharmacySystemService, IAdapterContext adapterContext)
        {
            _pharmacySystemService = pharmacySystemService;
            _adapterContext = adapterContext;
        }

        [HttpGet("{id}", Name = "GetDrugsFromPharmacy")]
        public async Task<ActionResult<List<DrugListDTO>>> GetDrugsFromPharmacy(int id)
        {
            List<DrugListDTO> ret = new List<DrugListDTO>();
            PharmacySystem pharmacySystem = await _pharmacySystemService.Get(id);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) != null)
                ret.AddRange(_adapterContext.PharmacySystemAdapter.GetAllDrugs());

            return Ok(ret);
        }
    }
}
