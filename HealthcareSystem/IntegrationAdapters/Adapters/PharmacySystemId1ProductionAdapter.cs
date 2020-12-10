
using AutoMapper;
using IntegrationAdapters.APIs;
using IntegrationAdapters.Dtos;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationAdapters.Adapters
{
    public class PharmacySystemId1ProductionAdapter : IPharmacySystemAdapter
    {
        private readonly PharmacySystemAdapterParameters _parameters;
        private readonly PharmacySystemId1RestApiClient _api;
        private readonly IMapper _mapper;

        public PharmacySystemId1ProductionAdapter(HttpClient client, PharmacySystemAdapterParameters parameters, IMapper mapper)
        {
            _parameters = parameters;
            _api = new PharmacySystemId1RestApiClient(client, _parameters.Url);
            _mapper = mapper;
        }

        public void CloseConnections()
        {
            _api._client.Dispose();
        }

        public List<DrugDto> DrugAvailibility(string name)
        {
            var drugs = _api.SearchDrugs(_parameters.ApiKey, name);
            return _mapper.Map<List<DrugDto>>(drugs);
        }
    }
}
