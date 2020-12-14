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
            ViewBag.PharmacyId = id;
            var drugList = _adapterContext.PharmacySystemAdapter.GetAllDrugs();
            _adapterContext.RemoveAdapter();

            return View(drugList);
        }

        public IActionResult RequestSpecifications(int pharmacyId, int drugId)
        {
            var pharmacySystem = _pharmacyService.GetPharmacyById(pharmacyId);
            _adapterContext.SetPharmacySystemAdapter(pharmacySystem);
            _adapterContext.PharmacySystemAdapter.GetDrugSpecifications(drugId);

            return RedirectToAction("DrugList", new { id = pharmacyId });
        }
    }
}
