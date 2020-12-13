﻿using AutoMapper;
using Grpc.Core;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.Protos;
using System;
using System.Collections.Generic;

namespace IntegrationAdapters.Adapters
{
    public class PharmacySystemId1DevelopementAdapter : IPharmacySystemAdapter
    {
        private readonly IMapper _mapper;
        private readonly PharmacySystemAdapterParameters _parameters;
        private Channel _grpcChannel;
        private DrugAvailability.DrugAvailabilityClient _grpcClient;

        public PharmacySystemId1DevelopementAdapter(PharmacySystemAdapterParameters parameters, IMapper mapper)
        {
            _parameters = parameters;
            _mapper = mapper;

            InitializeGrpc();
        }

        private void InitializeGrpc()
        {
            _grpcChannel = new Channel(_parameters.GrpcHost, _parameters.GrpcPort, ChannelCredentials.Insecure);
            _grpcClient = new DrugAvailability.DrugAvailabilityClient(_grpcChannel);
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
    }
}