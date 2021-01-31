using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersDrugService.Repository
{
    public interface IDrugConsumptionRepository
    {
        IEnumerable<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}