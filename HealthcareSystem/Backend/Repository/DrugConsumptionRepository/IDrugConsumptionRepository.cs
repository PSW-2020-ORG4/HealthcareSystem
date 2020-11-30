using System;
using System.Collections;
using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace Backend.Repository.DrugConsumptionRepository
{
    public interface IDrugConsumptionRepository
    {
        IEnumerable<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        void CreateDrugConsumption(DrugConsumption dc);
        void UpdateDrugConsumption(DrugConsumption dc);
        void DeleteDrugConsumption(DrugConsumption dc);
        IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateTime from, DateTime to);
    }
}