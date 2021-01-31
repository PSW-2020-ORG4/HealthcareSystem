using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdapters.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPush;

namespace IntegrationAdapters.Controllers
{
    public class DrugSpecificationsController : Controller
    {
        private readonly IPharmacySystemService _pharmacySystemService;
        private readonly IAdapterContext _adapterContext;
        private readonly IPushNotificationService _pushNotificationService;
        public DrugSpecificationsController(IPharmacySystemService pharmacySystemService,
                                            IAdapterContext adapterContext,
                                            IPushNotificationService pushNotificationService)
        {
            _pharmacySystemService = pharmacySystemService;
            _adapterContext = adapterContext;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pharmacySystemService.GetAll());
        }

        public async Task<IActionResult> DrugList(int id)
        {
            var pharmacySystem = await _pharmacySystemService.Get(id);
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
        public async Task<IActionResult> RequestSpecifications()
        {
            PushSubscription pushSubscription = new PushSubscription() { Endpoint = Request.Form["PushEndpoint"], P256DH = Request.Form["PushP256DH"], Auth = Request.Form["PushAuth"] };
            PushPayload pushPayload = new PushPayload();

            int pharmacyId = int.Parse(Request.Form["pharmacyId"]);
            int drugId = int.Parse(Request.Form["drugId"]);

            var pharmacySystem = await _pharmacySystemService.Get(pharmacyId);
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
