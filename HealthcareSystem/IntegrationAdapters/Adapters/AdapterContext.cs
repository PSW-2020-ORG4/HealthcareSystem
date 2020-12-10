using AutoMapper;
using Backend.Model.Pharmacies;
using System;
using System.Net.Http;

namespace IntegrationAdapters.Adapters
{
    public class AdapterContext : IAdapterContext
    {
        private IMapper _mapper;
        public PharmacySystem _pharmacySystem { get; private set; }
        private IPharmacySystemAdapter _pharmacySystemAdapter;
        private readonly string _environment;
        
        public AdapterContext(IMapper mapper)
        {
            _mapper = mapper;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem)
        {
            RemoveAdapter();
            _pharmacySystem = pharmacySystem;
            PharmacySystemAdapterParameters parameters = _mapper.Map<PharmacySystemAdapterParameters>(_pharmacySystem);

            if (_environment == "Development")
            {
                switch (_pharmacySystem.Id)
                {
                    case 1:
                        _pharmacySystemAdapter = new PharmacySystemId1DevelopementAdapter(parameters, _mapper);
                        break;
                    default:
                        _pharmacySystemAdapter = null;
                        break;
                }
            } 
            else
            {
                switch (pharmacySystem.Id)
                {
                    case 1:
                    _pharmacySystemAdapter = new PharmacySystemId1ProductionAdapter(parameters, _mapper);
                    break;
                    default:
                        _pharmacySystemAdapter = null;
                        break;
                }
            }

            return _pharmacySystemAdapter;
        }

        public IPharmacySystemAdapter GetPharmacySystemAdapter()
        {
            return _pharmacySystemAdapter;
        }

        public void RemoveAdapter()
        {
            if (_pharmacySystemAdapter != null)
                _pharmacySystemAdapter.CloseConnections();
            _pharmacySystemAdapter = null;
        }
    }
}