using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersService2.Service
{
    public interface IPharmacyService
    {
        List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);
    }
}
