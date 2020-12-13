using Backend.Model.Pharmacies;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IntegrationAdapters.Controllers
{
    public class DrugAvailabilityController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAdapterContext _adapterContext;
        private readonly IPharmacyService _pharmacyService;

        public DrugAvailabilityController(IAdapterContext adapterContext, IPharmacyService pharmacyService)
        {
            _adapterContext = adapterContext;
            _pharmacyService = pharmacyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string name)
        {
            var pharmacySystems = _pharmacyService.GetAllPharmacies();
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
