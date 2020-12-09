using AutoMapper;
using Backend.Model.Pharmacies;
using System;

namespace IntegrationAdapters.Adapters
{
    public class AdapterContext : IAdapterContext
    {
        public IMapper _mapper { get; }
        public PharmacySystem _pharmacySystem { get; private set; }
        private IPharmacySystemAdapter _pharmacySystemAdapter;
        private readonly string _environment;
        
        public AdapterContext(IMapper mapper)
        {
            _mapper = mapper;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public void SetPharmacySystemAdapter(PharmacySystem pharmacySystem)
        {
            Dispose();
            _pharmacySystem = pharmacySystem;
            PharmacySystemAdapterParameters parameters = _mapper.Map<PharmacySystemAdapterParameters>(_pharmacySystem);

            if (_environment == "Development")
            {
                switch (pharmacySystem.Id)
                {
                    case 1:
                        _pharmacySystemAdapter = new PharmacySystem_ID1_DevelopementAdapter(parameters, _mapper);
                        break;
                    default:
                        _pharmacySystem = null;
                        break;
                }
            } 
            else
            {
                switch (pharmacySystem.Id)
                {
                    case 1:
                    //_pharmacySystemAdapter = new PharmacySystem_ID1_ProductionAdapter(parameters, _mapper);
                    //break;
                    default:
                        _pharmacySystem = null;
                        break;
                }
            }
        }

        public IPharmacySystemAdapter GetPharmacySystemAdapter()
        {
            return _pharmacySystemAdapter;
        }

        public void Dispose()
        {
            if (_pharmacySystemAdapter != null)
                _pharmacySystemAdapter.Dispose();
        }
    }
}