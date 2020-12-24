using System.Collections.Generic;
using System.Linq;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.Services;
using Microsoft.AspNetCore.Mvc;
using WebPush;

namespace IntegrationAdapters.Controllers
{
    public class DrugSpecificationsController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IAdapterContext _adapterContext;
        private readonly IPushNotificationService _pushNotificationService;
        public DrugSpecificationsController(IPharmacyService pharmacyService,
                                            IAdapterContext adapterContext,
                                            IPushNotificationService pushNotificationService)
        {
            _pharmacyService = pharmacyService;
            _adapterContext = adapterContext;
            _pushNotificationService = pushNotificationService;
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

        [HttpPost]
        public IActionResult RequestSpecifications()
        {
            PushSubscription pushSubscription = new PushSubscription() { Endpoint = Request.Form["PushEndpoint"], P256DH = Request.Form["PushP256DH"], Auth = Request.Form["PushAuth"] };
            PushPayload pushPayload = new PushPayload();

            int pharmacyId = int.Parse(Request.Form["pharmacyId"]);
            int drugId = int.Parse(Request.Form["drugId"]);

            var pharmacySystem = _pharmacyService.GetPharmacyById(pharmacyId);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
            {
                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Error occured while downloading specification!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);
                return RedirectToAction("DrugList", new { id = pharmacyId });
            }

            if (_adapterContext.PharmacySystemAdapter.GetDrugSpecifications(drugId))
            {
                pushPayload.Title = "Success";
                pushPayload.Message = "Specification successfully downloaded!";
            }
            else
            {
                pushPayload.Title = "Unuccess";
                pushPayload.Message = "Error occured while downloading specification!";
                
            }
            _pushNotificationService.SendNotification(pushSubscription, pushPayload);

            return RedirectToAction("DrugList", new { id = pharmacyId });
        }
    }
}
