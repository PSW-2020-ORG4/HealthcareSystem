﻿using IntegrationAdapters.Dtos;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacySystemAdapter
    {
        public void CloseConnections();
        public void Initialize(PharmacySystemAdapterParameters parameters, HttpClient httpClient);
        public List<DrugDto> DrugAvailibility(string name);
        public bool SendDrugConsumptionReport(string reportFilePath, string reportFileName);
        public List<DrugListDTO> GetAllDrugs();
        public bool GetDrugSpecifications(int id);
        public bool OrderDrugs(int pharmacyId, int drugId, int quantity);
    }
}