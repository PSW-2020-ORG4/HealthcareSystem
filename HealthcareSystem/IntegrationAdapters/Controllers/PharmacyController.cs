using System;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class PharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPharmacySystemService _pharmacySystemService;

        public PharmacyController(IPharmacySystemService pharmacySystemService)
        {
            _pharmacySystemService = pharmacySystemService;
        }

        public IActionResult ApiRegister()
        {
            return View(new PharmacySystemDTO());
        }

        [HttpPost]
        public async Task<IActionResult> ApiRegister(PharmacySystemDTO pharmacyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pharmacyDto.ActionsBenefitsExchangeName != null)
                pharmacyDto.ActionsBenefitsSubscribed = true;

            try
            {
                PharmacySystem pharmacy = new PharmacySystem()
                {
                    Name = pharmacyDto.Name,
                    Url = pharmacyDto.Url,
                    ApiKey = pharmacyDto.ApiKey,
                    Email = pharmacyDto.Email,
                    ActionsBenefitsExchangeName = pharmacyDto.ActionsBenefitsExchangeName,
                    ActionsBenefitsSubscribed = pharmacyDto.ActionsBenefitsSubscribed,
                    GrpcAdress = new GrpcAdress(pharmacyDto.GrpcHost, pharmacyDto.GrpcPort)
                };
                await _pharmacySystemService.Create(pharmacy);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("ApiRegister");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pharmacySystemService.GetAll());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pharmacy = await _pharmacySystemService.Get(id);

            if (pharmacy == null)
                return NotFound("Pharmacy does not exist.");

            PharmacySystemDTO dto = new PharmacySystemDTO()
            {
                Id = pharmacy.Id,
                Name = pharmacy.Name,
                Url = pharmacy.Url,
                ApiKey = pharmacy.ApiKey,
                Email = pharmacy.Email,
                ActionsBenefitsExchangeName = pharmacy.ActionsBenefitsExchangeName,
                ActionsBenefitsSubscribed = pharmacy.ActionsBenefitsSubscribed,
                GrpcHost = pharmacy.GrpcAdress.GrpcHost,
                GrpcPort = pharmacy.GrpcAdress.GrpcPort
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PharmacySystemDTO pharmacyDto)
        {
            PharmacySystem pharmacy = new PharmacySystem()
            {
                Id = pharmacyDto.Id,
                Name = pharmacyDto.Name,
                Url = pharmacyDto.Url,
                ApiKey = pharmacyDto.ApiKey,
                Email = pharmacyDto.Email,
                ActionsBenefitsExchangeName = pharmacyDto.ActionsBenefitsExchangeName,
                ActionsBenefitsSubscribed = pharmacyDto.ActionsBenefitsSubscribed,
                GrpcAdress = new GrpcAdress(pharmacyDto.GrpcHost, pharmacyDto.GrpcPort)
            };
            if (await _pharmacySystemService.Update(pharmacy))
                return RedirectToAction("Index");
            return BadRequest("Something went wrong");
        }
    }
}