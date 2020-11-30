using System;
using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace Backend.Service.DrugConsumptionService
{
    public interface IDrugConsumptionService
    {
        List<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        void CreateDrugConsumption(DrugConsumption dc);
        void UpdateDrugConsumption(DrugConsumption dc);
        void DeleteDrugConsumption(DrugConsumption dc);
        List<DrugConsumptionReport> GetDrugConsumptionForDate(DateTime from, DateTime to);
    }
}