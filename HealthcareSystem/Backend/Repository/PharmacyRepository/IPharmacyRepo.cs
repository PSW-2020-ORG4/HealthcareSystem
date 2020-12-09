using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IPharmacyRepo
    {
        bool SaveChanges();

        IEnumerable<PharmacySystem> GetAllPharmacies();
        PharmacySystem GetPharmacyById(int id);
        PharmacySystem GetPharmacyByExchangeName(string exchangeName);
        IEnumerable<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed);

        void CreatePharmacy(PharmacySystem p);
        void UpdatePharmacy(PharmacySystem p);
        void DeletePharmacy(PharmacySystem p);
    }
}
