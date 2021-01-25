using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.Controllers
{
    public class DrugAvailabilityController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IPharmacySystemService _pharmacySystemService;

        public DrugAvailabilityController(IAdapterContext adapterContext, IPharmacySystemService pharmacySystemService)
        {
            _adapterContext = adapterContext;
            _pharmacySystemService = pharmacySystemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async  Task<IActionResult> Search(string name)
        {
            var pharmacySystems = await _pharmacySystemService.GetAll();
            List<SearchResultDto> result = new List<SearchResultDto>();
            foreach (PharmacySystem pharmacySystem in pharmacySystems)
            {
                if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
                    continue;

                List<DrugDto> search = new List<DrugDto> ();
                search.AddRange(_adapterContext.PharmacySystemAdapter.DrugAvailibility(name));
                
                if(search.Count > 0)
                    result.Add(new SearchResultDto() { pharmacySystem = pharmacySystem, drugs = new List<DrugDto>(search) });
            }
            _adapterContext.RemoveAdapter();
            ViewBag.SearchBox = name;
            return View(result);
        }
    }
}
