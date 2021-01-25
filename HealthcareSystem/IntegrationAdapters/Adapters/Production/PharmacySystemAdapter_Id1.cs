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
            Task<List<DrugDto>> task = Task.Run<List<DrugDto>>(async () => await _api.SearchDrugs(_parameters.ApiKey, name));
            List<DrugDto> ret = new List<DrugDto>();
            try
            {
                ret = task.Result;
            }
            catch(AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            
            return ret;
        }

        public bool SendDrugConsumptionReport(string reportFilePath, string reportFileName)
        { 
            Task<bool> task = Task.Run<bool>(async () => await _api.SendDrugConsumptionRepor(_parameters.ApiKey, reportFilePath, reportFileName));
            bool ret = false;
            try
            {
                ret = task.Result;
            }
            catch(AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            return ret;
        }

        public List<DrugListDTO> GetAllDrugs()
        {
            var task =
                Task.Run<List<DrugListDTO>>(async () => await _api.GetAllDrugs(_parameters.ApiKey));
            var ret = new List<DrugListDTO>();

            try
            {
                ret = task.Result;
            }
            catch (AggregateException agex)
            {
                throw new Exception("Pharmacy not available. Please try again later.");
            }

            return ret;
        }

        public bool GetDrugSpecifications(int id)
        {
            var task = Task.Run<bool>(async () => await _api.GetDrugSpecificationsHttp(_parameters.ApiKey, id));
            bool ret = false;
            try
            {
                ret = task.Result;
            }
            catch (AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            return ret;
        }

        public bool OrderDrugs(int pharmacyId, int drugId, int quantity)
        {
            var task = Task.Run<bool>(async () =>
                await _api.OrderDrugs(_parameters.ApiKey, pharmacyId, drugId, quantity));
            bool ret = false;
            try
            {
                ret = task.Result;
            }
            catch (AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            return ret;
        }
    }
}
