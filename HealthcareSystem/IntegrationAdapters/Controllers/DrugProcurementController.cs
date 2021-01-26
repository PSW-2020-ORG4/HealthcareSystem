using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntegrationAdapters.Controllers
{
    public class DrugProcurementController : Controller
    {
        private readonly IDrugService _drugService;
        private readonly IAdapterContext _adapterContext;
        private readonly IPharmacySystemService _pharmacySystemService;
        public DrugProcurementController(IDrugService drugService, IAdapterContext adapterContext, IPharmacySystemService pharmacySystemService)
        {
            _drugService = drugService;
            _adapterContext = adapterContext;
            _pharmacySystemService = pharmacySystemService;
        }

        public IActionResult Index(DrugProcurementDTO drugProcurementDto)
        {
            return View(drugProcurementDto);
        }

        public async Task<IActionResult> Order(DrugProcurementDTO drugProcurementDto)
        {
            var pharmacySystem = await _pharmacySystemService.Get(drugProcurementDto.PharmacySystemId);
            if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
                throw new ArgumentNullException("There is no pharmacy with id=" + drugProcurementDto.PharmacySystemId);

            if (_adapterContext.PharmacySystemAdapter.OrderDrugs(drugProcurementDto.PharmacyId,
                                                                 drugProcurementDto.DrugId,
                                                                 drugProcurementDto.Quantity))
            {
                if (await _drugService.AddQuantity(drugProcurementDto.Code, drugProcurementDto.Quantity))
                {
                    TempData["Success"] = "Order successful!";
                }
                else
                {
                    TempData["Failure"] = "Drug procured but does not exsist in our database, contact administrator!";
                }
            }
            else
            {
                TempData["Failure"] = "Order unsuccessful!";
            }

            return RedirectToAction("Index", drugProcurementDto);
        }
    }
}
