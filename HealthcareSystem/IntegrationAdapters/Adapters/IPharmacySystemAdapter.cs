using IntegrationAdapters.Dtos;
using System;
using System.Collections.Generic;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacySystemAdapter
    {
        public void CloseConnections();
        public List<DrugDTO> DrugAvailibility(string name);
    }
}