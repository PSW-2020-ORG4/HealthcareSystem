using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Model.Pharmacies;
using Backend.Repository.DrugConsumptionRepository;

namespace Backend.Service.DrugConsumptionService
{
    public class DrugConsumptionService : IDrugConsumptionService
    {
        private readonly IDrugConsumptionRepository _drugConsumptionRepository;

        public DrugConsumptionService(IDrugConsumptionRepository drugConsumptionRepository)
        {
            _drugConsumptionRepository = drugConsumptionRepository;
        }
        public List<DrugConsumption> GetAllDrugConsumptions()
        {
            return _drugConsumptionRepository.GetAllDrugConsumptions().ToList();
        }

        public DrugConsumption GetDrugConsumptionById(int id)
        {
            return _drugConsumptionRepository.GetDrugConsumptionById(id);
        }

        public void CreateDrugConsumption(DrugConsumption dc)
        {
            _drugConsumptionRepository.CreateDrugConsumption(dc);
        }

        public void UpdateDrugConsumption(DrugConsumption dc)
        {
            _drugConsumptionRepository.UpdateDrugConsumption(dc);
        }

        public void DeleteDrugConsumption(DrugConsumption dc)
        {
            _drugConsumptionRepository.DeleteDrugConsumption(dc);
        }

        public List<DrugConsumptionReport> GetDrugConsumptionForDate(DateTime from, DateTime to)
        {
            return _drugConsumptionRepository.GetDrugConsumptionForDate(from, to).ToList();
        }
    }
}