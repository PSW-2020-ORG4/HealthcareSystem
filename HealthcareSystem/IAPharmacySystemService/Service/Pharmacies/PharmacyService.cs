using Backend.Model.Pharmacies;
using IntegrationAdaptersPharmacySystemService.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersPharmacySystemService.Service
{
    public class PharmacyService : IPharmacyService
    {
        private IPharmacyRepo _pharmacyRepo;

        public PharmacyService(IPharmacyRepo pharmacyRepo)
        {
            _pharmacyRepo = pharmacyRepo;
        }

        public void CreatePharmacy(PharmacySystem p)
        {
            _pharmacyRepo.CreatePharmacy(p);
        }

        public IEnumerable<PharmacySystem> GetAllPharmacies()
        {
            return _pharmacyRepo.GetAllPharmacies();
        }

        public List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _pharmacyRepo.GetPharmaciesBySubscribed(subscribed).ToList();
        }

        public PharmacySystem GetPharmacyById(int id)
        {
            return _pharmacyRepo.GetPharmacyById(id);
        }

        public PharmacySystem GetPharmacyByIdNoTracking(int id)
        {
            return _pharmacyRepo.GetPharmacyByIdNoTracking(id);
        }

        public void UpdatePharmacy(PharmacySystem p)
        {
            _pharmacyRepo.UpdatePharmacy(p);
        }
    }
}
