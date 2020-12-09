using IntegrationAdapters.Dtos;
using System;
using System.Collections.Generic;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacySystemAdapter : IDisposable
    {
        public List<DrugDTO> DrugAvailibility(string name);
        //public void Tender();
    }
}