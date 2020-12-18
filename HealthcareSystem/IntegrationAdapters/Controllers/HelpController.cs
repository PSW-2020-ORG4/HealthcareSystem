using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using Backend.Service;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelpController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IAdapterContext _adapterContext;

        public HelpController(IPatientService patientService, IPharmacyService pharmacyService, IAdapterContext adapterContext)
        {
            _patientService = patientService;
            _pharmacyService = pharmacyService;
            _adapterContext = adapterContext;
        }

        [HttpGet("{id}", Name = "GetDrugsFromPharmacy")]
        public ActionResult<List<DrugListDTO>> GetDrugsFromPharmacy(int id)
        {
            List<DrugListDTO> ret = new List<DrugListDTO>();
            PharmacySystem pharmacySystem = _pharmacyService.GetPharmacyById(id);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) != null)
                ret.AddRange(_adapterContext.PharmacySystemAdapter.GetAllDrugs());

            return Ok(ret);
        }
    }
}
