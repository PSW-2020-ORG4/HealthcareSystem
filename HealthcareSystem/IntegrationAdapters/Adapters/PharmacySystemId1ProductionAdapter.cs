
using AutoMapper;
using IntegrationAdapters.APIs;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.Protos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.Adapters
{
    public class PharmacySystemId1ProductionAdapter : IPharmacySystemAdapter
    {
        private readonly PharmacySystemAdapterParameters _parameters;
        private readonly PharmacySystemId1RestApiClient _api;
        private readonly IMapper _mapper;

        public PharmacySystemId1ProductionAdapter(PharmacySystemAdapterParameters parameters, IMapper mapper)
        {
            _parameters = parameters;
            _mapper = mapper;
            _api = new PharmacySystemId1RestApiClient(_parameters.Url);
            
        }

        public void CloseConnections()
        {
            _api.DisposeClient();
        }

        public List<DrugDto> DrugAvailibility(string name)
        {
            Task<List<Drug>> task = Task.Run<List<Drug>>(async () => await _api.SearchDrugs(_parameters.ApiKey, name));
            List<Drug> ret = new List<Drug>();
            try
            {
                ret = task.Result;
            }
            catch(AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            
            return _mapper.Map<List<DrugDto>>(ret);
        }
    }
}
