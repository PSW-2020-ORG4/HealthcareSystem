using Backend.Model.Exceptions;
using Backend.Model.Pharmacies;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
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
            List<SearchResultDTO> result = new List<SearchResultDTO>();
            foreach (PharmacySystem pharmacySystem in pharmacySystems)
            {
                if (_adapterContext.SetPharmacySystemAdapter(pharmacySystem) == null)
                    continue;

                List<DrugDTO> search = null;

                try
                {
                 search = _adapterContext.GetPharmacySystemAdapter().DrugAvailibility(name);
                } 
                catch(GrpcException gEx)
                {
                    Console.WriteLine(gEx);
                }

                if(search != null)
                    result.Add(new SearchResultDTO() { pharmacySystem = pharmacySystem, drugs = new List<DrugDTO>(search) });
            }
            _adapterContext.RemoveAdapter();
            ViewBag.SearchBox = name;
            return View(result);
        }
    }
}
