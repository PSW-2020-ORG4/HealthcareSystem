using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace Backend.Service.DrugConsumptionService
{
    public interface IDrugConsumptionService
    {
        List<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        List<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}