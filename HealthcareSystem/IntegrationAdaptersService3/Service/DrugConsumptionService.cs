using System.Collections.Generic;
using System.Linq;
using Backend.Model.Pharmacies;
using IntegrationAdaptersService3.Repository;

namespace IntegrationAdaptersService3.Service
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