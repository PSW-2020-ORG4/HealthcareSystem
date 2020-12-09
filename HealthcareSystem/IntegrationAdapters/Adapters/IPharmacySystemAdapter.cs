using IntegrationAdapters.Dtos;
using System.Collections.Generic;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacySystemAdapter
    {
        public void CloseConnections();
        public List<DrugDto> DrugAvailibility(string name);
    }
}