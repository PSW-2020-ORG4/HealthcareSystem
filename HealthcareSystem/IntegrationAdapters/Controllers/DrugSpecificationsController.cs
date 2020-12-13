using System.Collections.Generic;
using System.Linq;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class DrugSpecificationsController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IAdapterContext _adapterContext;

        public DrugSpecificationsController(IPharmacyService pharmacyService, IAdapterContext adapterContext)
        {
            _pharmacyService = pharmacyService;
            _adapterContext = adapterContext;
        }
        public IActionResult Index()
        {
            return View(_pharmacyService.GetAllPharmacies().ToList());
        }

        public IActionResult DrugList(int id)
        {
            var pharmacySystem = _pharmacyService.GetPharmacyById(id);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
            {
                return View(new List<DrugListDTO>());
            }

            var drugList = _adapterContext.PharmacySystemAdapter.GetAllDrugs();
            _adapterContext.RemoveAdapter();

            return View(drugList);
        }

        public IActionResult RequestSpecifications(int id)
        {
            var pharmacySystem = _pharmacyService.GetPharmacyById(1);
            _adapterContext.SetPharmacySystemAdapter(pharmacySystem);
            var success = _adapterContext.PharmacySystemAdapter.GetDrugSpecifications(id);

            return RedirectToAction("Index");
        }
    }
}
