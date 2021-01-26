using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersDrugService.Service
{
    public interface IDrugConsumptionService
    {
        List<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        List<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}