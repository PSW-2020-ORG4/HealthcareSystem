using Backend.Model.Pharmacies;
using Backend.Repository.DrugConsumptionRepository;
using System.Collections.Generic;
using System.Linq;

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

        public List<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange)
        {
            return _drugConsumptionRepository.GetDrugConsumptionForDate(dateRange).ToList();
        }
    }
}