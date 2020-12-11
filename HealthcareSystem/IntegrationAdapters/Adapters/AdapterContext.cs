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

        public AdapterContext(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new PharmacySystemProfile()));
            _mapper = new Mapper(mapperConfig);
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem)
        {
            RemoveAdapter();
            _pharmacySystem = pharmacySystem;
            PharmacySystemAdapterParameters parameters = _mapper.Map<PharmacySystemAdapterParameters>(_pharmacySystem);

            try
            {
                if (_environment == "Development")
                {
                    PharmacySystemAdapter = (IPharmacySystemAdapter)Activator.CreateInstance(Type.GetType($"IntegrationAdapters.Adapters.Development.PharmacySystemAdapter_Id{_pharmacySystem.Id}"));
                    PharmacySystemAdapter.Initialize(parameters, null);
                }
                else
                {
                    PharmacySystemAdapter = (IPharmacySystemAdapter)Activator.CreateInstance(Type.GetType($"IntegrationAdapters.Adapters.Production.PharmacySystemAdapter_Id{_pharmacySystem.Id}"));
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