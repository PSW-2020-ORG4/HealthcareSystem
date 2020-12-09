using AutoMapper;
using Backend.Model.Pharmacies;
using System;

namespace IntegrationAdapters.Adapters
{
    public class AdapterContext : IAdapterContext
    {
        public IMapper _mapper { get; }
        public PharmacySystem _pharmacy { get; private set; }
        private IPharmacySystemAdapter _pharmacySystemAdapter;
        private readonly string _environment;
        
        public AdapterContext(IMapper mapper)
        {
            _mapper = mapper;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public void SetPharmacySystemAdapter(PharmacySystem pharmacy)
        {
            Dispose();
            _pharmacy = pharmacy;
            PharmacySystemAdapterParameters parameters = _mapper.Map<PharmacySystemAdapterParameters>(pharmacy);

            if (_environment == "Development")
            {
                switch (pharmacy.Id)
                {
                    case 1:
                        _pharmacySystemAdapter = new PharmacySystem_ID1_DevelopementAdapter(parameters, _mapper);
                        break;
                    default:
                        _pharmacy = null;
                        break;
                }
            } 
            else
            {
                switch (pharmacy.Id)
                {
                    case 1:
                    //_pharmacySystemAdapter = new PharmacySystem_ID1_ProductionAdapter(parameters, _mapper);
                    //break;
                    default:
                        _pharmacy = null;
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