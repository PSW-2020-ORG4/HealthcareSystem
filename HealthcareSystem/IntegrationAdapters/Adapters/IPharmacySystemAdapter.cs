using IntegrationAdapters.Dtos;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacySystemAdapter
    {
        public void CloseConnections();
        public void Initialize(PharmacySystemAdapterParameters parameters, HttpClient _httpClient);
        public List<DrugDto> DrugAvailibility(string name);
    }
}