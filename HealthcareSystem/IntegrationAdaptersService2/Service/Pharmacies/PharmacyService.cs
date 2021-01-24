using Backend.Model.Pharmacies;
using IntegrationAdaptersService2.Repository;
using System.Collections.Generic;
using System.Linq;


namespace IntegrationAdaptersService2.Service
{
    public class PharmacyService : IPharmacyService
    {
        private IPharmacyRepo _pharmacyRepo;

        public PharmacyService(IPharmacyRepo pharmacyRepo)
        {
            _pharmacyRepo = pharmacyRepo;
        }

        public List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _pharmacyRepo.GetPharmaciesBySubscribed(subscribed).ToList();
        }
    }
}
