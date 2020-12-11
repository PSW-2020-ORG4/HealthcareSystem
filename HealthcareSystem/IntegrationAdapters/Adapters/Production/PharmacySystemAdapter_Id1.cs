using AutoMapper;
using IntegrationAdapters.Apis.Grpc;
using IntegrationAdapters.Apis.Http;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationAdapters.Adapters.Production
{
    public class PharmacySystemAdapter_Id1 : IPharmacySystemAdapter
    {
        private IMapper _mapper;
        private PharmacySystemAdapterParameters _parameters;
        private PharmacySystemApi_Id1 _api;

        public void Initialize(PharmacySystemAdapterParameters parameters, HttpClient httpClient)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new PharmacySystemProfile_Id1()));
            _mapper = new Mapper(mapperConfig);
            _parameters = parameters;
            _api = new PharmacySystemApi_Id1(_parameters.Url, httpClient);
        }

        public void CloseConnections()
        {
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
