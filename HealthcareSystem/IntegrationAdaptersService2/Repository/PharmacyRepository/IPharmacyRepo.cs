using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersService2.Repository
{
    public interface IPharmacyRepo
    {
        PharmacySystem GetPharmacyByExchangeName(string exchangeName);
        IEnumerable<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);
    }
}
