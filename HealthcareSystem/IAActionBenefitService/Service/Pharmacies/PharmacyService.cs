using Backend.Model.Pharmacies;
using IntegrationAdaptersActionBenefitService.Repository;
using System.Collections.Generic;
using System.Linq;


namespace IntegrationAdaptersActionBenefitService.Service
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

        public PharmacySystem GetPharmacyByExchangeName(string exchangeName)
        {
            return _pharmacyRepo.GetPharmacyByExchangeName(exchangeName);
        }
    }
}
