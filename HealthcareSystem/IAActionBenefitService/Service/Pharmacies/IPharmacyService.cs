using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersActionBenefitService.Service
{
    public interface IPharmacyService
    {
        List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);
        PharmacySystem GetPharmacyByExchangeName(string exchangeName);
    }
}
