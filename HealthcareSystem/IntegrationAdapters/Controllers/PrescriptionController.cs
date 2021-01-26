using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdapters.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebPush;

namespace IntegrationAdapters.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPharmacySystemService _pharmacySystemService;
        private readonly IAdapterContext _adapterContext;
        private readonly IPushNotificationService _pushNotificationService;

        public PrescriptionController(IPrescriptionService prescriptionService,
                                      IPharmacySystemService pharmacySystemService,
                                      IAdapterContext adapterContext,
                                      IPushNotificationService pushNotificationService)
        {
            _prescriptionService = prescriptionService;
            _pharmacySystemService = pharmacySystemService;
            _adapterContext = adapterContext;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            PrescriptionView view = new PrescriptionView();
            view.patients = await _prescriptionService.GetAllPatients();
            view.pharmacySystems = await _pharmacySystemService.GetAll();

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> SendPrescription(PrescriptionForm form)
        {
            PushSubscription pushSubscription = new PushSubscription() { Endpoint = Request.Form["PushEndpoint"], P256DH = Request.Form["PushP256DH"], Auth = Request.Form["PushAuth"] };
            PushPayload pushPayload = new PushPayload();

            if (!FormValid(form))
            {
                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Invalid prescription data!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);

                return RedirectToAction("Index");
            }

            var reportFilePath = "Resources";
            string reportFileName = Guid.NewGuid().ToString() + ".json";
            var prescription = new { drugId = form.Drug, jmbg = form.Patient };
            string json = JsonConvert.SerializeObject(prescription, Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(reportFilePath + "/" + reportFileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                pushPayload.Title = "Unsuccess";
                pushPayload.Message = "Error occured while creating prescription file!";
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);
            }
            var pharmacy = await _pharmacySystemService.Get(form.PharmacySystem);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacy) != null)
            {
                if (_adapterContext.PharmacySystemAdapter.SendDrugConsumptionReport(reportFilePath, reportFileName))
                {
                    pushPayload.Title = "Success";
                    pushPayload.Message = "Prescription successfully created and uploaded!";
                }
                else
                {
                    pushPayload.Title = "Unsuccess";
                    pushPayload.Message = "Prescription upload to " + pharmacy.Name + @" unsuccessfull!""}";
                }
                _pushNotificationService.SendNotification(pushSubscription, pushPayload);
            }
            return RedirectToAction("Index");
        }

        private bool FormValid(PrescriptionForm form)
        {
            if (form.Drug == 0 || form.Patient == null || form.Patient == "" || form.PharmacySystem == 0)
                return false;

            return true;
        }
    }
}
