using Backend.Model.Pharmacies;

namespace IntegrationAdapters.Adapters
{
    public interface IAdapterContext
    {
        IPharmacySystemAdapter PharmacySystemAdapter { get; }

        public IPharmacySystemAdapter SetPharmacySystemAdapter(PharmacySystem pharmacySystem);
        public void RemoveAdapter();
    }
}
