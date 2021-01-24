using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersService3.Service
{
    public interface IDrugConsumptionService
    {
        List<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        List<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}