using Backend.Model.Pharmacies;
using System.Collections.Generic;


namespace IntegrationAdaptersPharmacySystemService.Service
{
    public interface IPharmacyService
    {
        IEnumerable<PharmacySystem> GetAllPharmacies();
        PharmacySystem GetPharmacyById(int id);
        PharmacySystem GetPharmacyByIdNoTracking(int id);
        List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);

        void CreatePharmacy(PharmacySystem p);
        void UpdatePharmacy(PharmacySystem p);
    }
}
