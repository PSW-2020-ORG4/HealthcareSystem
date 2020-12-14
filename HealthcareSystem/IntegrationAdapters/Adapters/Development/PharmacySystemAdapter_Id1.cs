using AutoMapper;
using Backend.Communication.SftpCommunicator;
using Grpc.Core;
using IntegrationAdapters.Apis.Grpc;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MapperProfiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationAdapters.Apis.Http;

namespace IntegrationAdapters.Adapters.Development
{
    public class PharmacySystemAdapter_Id1 : IPharmacySystemAdapter
    {
        private  IMapper _mapper;
        private PharmacySystemAdapterParameters _parameters;
        private Channel _grpcChannel;
        private DrugAvailability.DrugAvailabilityClient _grpcClient;
        private SftpCommunicator _sftpCommunicator;
        private bool _grpc;
        private bool _sftp;
        private PharmacySystemApi_Id1 _api;

        public void Initialize(PharmacySystemAdapterParameters parameters, HttpClient httpClient)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new PharmacySystemProfile_Id1()));
            _mapper = new Mapper(mapperConfig);
            _parameters = parameters;
            InitializeGrpc();
            InitializeSftp();
            _api = new PharmacySystemApi_Id1(_parameters.Url, httpClient);
        }

        public void CloseConnections()
        {
            if (_grpcChannel != null && _grpcChannel.State != ChannelState.Shutdown)
                _grpcChannel.ShutdownAsync().Wait();
        }

        public List<DrugDto> DrugAvailibility(string name)
        {
            if (!_grpc)
                return new List<DrugDto>();

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

        public bool SendDrugConsumptionReport(string reportFilePath, string reportFileName)
        {
            if (!_sftp)
                return false;

            Task<bool> task = Task.Run<bool>(async () => await _sftpCommunicator.UploadFile(reportFilePath + "/" + reportFileName, $"/PSW-uploads/{reportFileName}"));
            bool ret = false;
            try
            {
                ret = task.Result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
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
                Console.WriteLine(agex);
            }

            return ret;
        }

        public bool GetDrugSpecifications(int id)
        {
            var task = Task.Run<string>(async () => await _api.GetDrugSpecificationsSftp(_parameters.ApiKey, id));

            var ret = "";
            try
            {
                ret = task.Result;
            }
            catch (AggregateException agex)
            {
                Console.WriteLine(agex);
            }

            if (ret == "") return false;

            _sftpCommunicator.DownloadFile(ret, $"Resources/{Path.GetFileName(ret)}");
            return true;
        }

        private void InitializeGrpc()
        {   if(_parameters.GrpcHost == null || _parameters.GrpcHost == "" || _parameters.GrpcPort == -1)
                _grpc = false;
            else
                _grpc = true;

            _grpcChannel = new Channel(_parameters.GrpcHost, _parameters.GrpcPort, ChannelCredentials.Insecure);
            _grpcClient = new DrugAvailability.DrugAvailabilityClient(_grpcChannel);

        }

        private void InitializeSftp()
        {
            if (_parameters.SftpConfig == null)
                _sftp = false;
            else
                _sftp = true;

            _sftpCommunicator = new SftpCommunicator(_parameters.SftpConfig);
        }
    }
}
