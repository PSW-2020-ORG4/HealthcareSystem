﻿using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersDrugService.Repository
{
    public interface IDrugConsumptionRepository
    {
        IEnumerable<DrugConsumption> GetAllDrugConsumptions();
        DrugConsumption GetDrugConsumptionById(int id);
        IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange);
    }
}