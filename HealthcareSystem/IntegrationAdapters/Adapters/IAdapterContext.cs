using Backend.Model.Pharmacies;
using System;

namespace IntegrationAdapters.Adapters
{
    public interface IAdapterContext : IDisposable
    {
        public void SetPharmacySystemAdapter(PharmacySystem pharmacy);
        public IPharmacySystemAdapter GetPharmacySystemAdapter();
    }
}
