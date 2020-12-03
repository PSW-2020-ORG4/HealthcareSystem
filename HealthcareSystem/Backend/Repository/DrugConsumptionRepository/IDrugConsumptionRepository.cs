﻿using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace Backend.Repository.DrugConsumptionRepository
{
    public interface IDrugConsumptionRepository
    {
        IEnumerable<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        void CreateDrugConsumption(DrugConsumption dc);
        void UpdateDrugConsumption(DrugConsumption dc);
        void DeleteDrugConsumption(DrugConsumption dc);
        IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}