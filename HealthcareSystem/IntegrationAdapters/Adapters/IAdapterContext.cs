using Backend.Model.Pharmacies;
using System;

namespace IntegrationAdapters.Adapters
{
    public interface IAdapterContext
    {
        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem);
        public IPharmacySystemAdapter GetPharmacySystemAdapter();
        public void RemoveAdapter();
    }
}
