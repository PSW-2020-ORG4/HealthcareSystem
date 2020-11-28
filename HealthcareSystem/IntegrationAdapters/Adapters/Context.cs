namespace IntegrationAdapters.Adapters
{
    public class Context
    {
        public IPharmacyAdapter PharmacyAdapter { get; set; }

        public Context(IPharmacyAdapter iPharmacy)
        {
            PharmacyAdapter = iPharmacy;
        }

        public void CheckMedicine()
        {
            PharmacyAdapter.CheckMedicine();
        }

        public void Tender()
        {
            PharmacyAdapter.Tender();
        }
    }
}