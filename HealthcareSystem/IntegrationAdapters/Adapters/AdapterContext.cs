using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.MapperProfiles;
using System;
using System.Net.Http;

namespace IntegrationAdapters.Adapters
{
    public class AdapterContext : IAdapterContext
    {
        private readonly IMapper _mapper;
        public PharmacySystem _pharmacySystem { get; private set; }
        public IPharmacySystemAdapter PharmacySystemAdapter { get; private set; }
        private readonly string _environment;
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly string _hospitalName = "HealthcareSystem-ORG4";

        public AdapterContext(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new PharmacySystemProfile()));
            _mapper = new Mapper(mapperConfig);
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //_environment = "Development";
            //_environment = "Production";
        }

        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem)
        {
            RemoveAdapter();
            _pharmacySystem = pharmacySystem;
            PharmacySystemAdapterParameters parameters = _mapper.Map<PharmacySystemAdapterParameters>(_pharmacySystem);
            parameters.HospitalName = _hospitalName;

            try
            {
                PharmacySystemAdapter = (IPharmacySystemAdapter)Activator.CreateInstance(Type.GetType($"IntegrationAdapters.Adapters.{_environment}.PharmacySystemAdapter_Id{_pharmacySystem.Id}"));
                if (_environment == "Development")
                {
                    var config = Startup.Configuration.GetSection("SftpConfig");
                    parameters.SftpConfig = new SftpConfig() 
                    { 
                        Host = config["Host"],
                        Port = int.Parse(config["Port"]), 
                        Username = config["Username"], 
                        Password = config["Password"] 
                    };
                    
                    PharmacySystemAdapter.Initialize(parameters, _httpClientFactory.CreateClient());
                }
                else if(_environment == "Production")
                {
                    PharmacySystemAdapter.Initialize(parameters, _httpClientFactory.CreateClient());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                PharmacySystemAdapter = null;
            }

            return PharmacySystemAdapter;
        }

        public void RemoveAdapter()
        {
            if (PharmacySystemAdapter != null)
                PharmacySystemAdapter.CloseConnections();
            PharmacySystemAdapter = null;
        }
    }
}