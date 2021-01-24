using Backend.Model.Pharmacies;
using System.Collections.Generic;


namespace IntegrationAdaptersPharmacySystemService.Repository
{
    public interface IPharmacyRepo
    {
        bool SaveChanges();

        IEnumerable<PharmacySystem> GetAllPharmacies();
        PharmacySystem GetPharmacyById(int id);
        PharmacySystem GetPharmacyByIdNoTracking(int id);
        IEnumerable<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);

        void CreatePharmacy(PharmacySystem p);
        void UpdatePharmacy(PharmacySystem p);
    }
}
