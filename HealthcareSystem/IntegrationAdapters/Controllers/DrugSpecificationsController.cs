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

        [Route("DrugSpecifications/DrugList/{idP}/{idD}")]
        public IActionResult RequestSpecifications(int idD, int idP)
        {
            var pharmacySystem = _pharmacyService.GetPharmacyById(idP);
            _adapterContext.SetPharmacySystemAdapter(pharmacySystem);
            var success = _adapterContext.PharmacySystemAdapter.GetDrugSpecifications(idD);

            return RedirectToAction("Index");
        }
    }
}
