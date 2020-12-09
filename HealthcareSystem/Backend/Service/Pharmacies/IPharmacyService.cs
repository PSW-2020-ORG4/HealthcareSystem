using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Pharmacies
{
    public interface IPharmacyService
    {
        IEnumerable<PharmacySystem> GetAllPharmacies();
        PharmacySystem GetPharmacyById(int id);
        PharmacySystem GetPharmacyByExchangeName(string exchangeName);
        List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);

        void SubscribeUnsubscibe(int id, bool subscribe);
        void CreatePharmacy(PharmacySystem p);
        void UpdatePharmacy(PharmacySystem p);
        void DeletePharmacy(PharmacySystem p);
    }
}
