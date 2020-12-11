using Backend.Model.Pharmacies;

namespace IntegrationAdapters.Adapters
{
    public interface IAdapterContext
    {
        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem);
        public IPharmacySystemAdapter GetPharmacySystemAdapter();
        public void RemoveAdapter();
    }
}
