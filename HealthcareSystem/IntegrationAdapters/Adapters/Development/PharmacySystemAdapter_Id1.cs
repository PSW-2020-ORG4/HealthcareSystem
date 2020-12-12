using AutoMapper;
using Grpc.Core;
using IntegrationAdapters.Apis.Grpc;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace IntegrationAdapters.Adapters.Development
{
    public class PharmacySystemAdapter_Id1 : IPharmacySystemAdapter
    {
        private readonly IMapper _mapper;
        private PharmacySystemAdapterParameters _parameters;
        private Channel _grpcChannel;
        private DrugAvailability.DrugAvailabilityClient _grpcClient;

        public PharmacySystemAdapter_Id1()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new PharmacySystemProfile_Id1()));
            _mapper = new Mapper(mapperConfig);
        }

        public void Initialize(PharmacySystemAdapterParameters parameters, HttpClient httpClient)
        {
            _parameters = parameters;
            InitializeGrpc();
        }

        public void CloseConnections()
        {
            if (_grpcChannel != null && _grpcChannel.State != ChannelState.Shutdown)
                _grpcChannel.ShutdownAsync().Wait();
        }

        public List<DrugDto> DrugAvailibility(string name)
        {
            FindDrugRequest request = new FindDrugRequest();
            request.ApiKey = _parameters.ApiKey;
            request.Name = name;
            FindDrugResponse response = null;
            try
            {
                response = _grpcClient.FindDrug(request);
            }
            catch(RpcException rex)
            {
                Console.WriteLine(rex);
            }
            if (response != null && response.Drugs != null && response.Drugs.Count > 0)
            {
                return _mapper.Map<List<DrugDto>>(response.Drugs);
            }

            return new List<DrugDto>();
        }

        private void InitializeGrpc()
        {
            _grpcChannel = new Channel(_parameters.GrpcHost, _parameters.GrpcPort, ChannelCredentials.Insecure);
            _grpcClient = new DrugAvailability.DrugAvailabilityClient(_grpcChannel);
        }

        public bool SendDrugConsumptionRepor(string reportFIleName)
        {
            throw new NotImplementedException();
        }
    }
}
