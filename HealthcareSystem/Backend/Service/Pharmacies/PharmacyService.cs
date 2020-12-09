using Backend.Model.Pharmacies;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Pharmacies
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

        public void DeletePharmacy(PharmacySystem p)
        {
            _pharmacyRepo.DeletePharmacy(p);
        }

        public IEnumerable<PharmacySystem> GetAllPharmacies()
        {
            return _pharmacyRepo.GetAllPharmacies();
        }

        public List<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _pharmacyRepo.GetPharmaciesBySubscribed(subscribed).ToList();
        }

        public PharmacySystem GetPharmacyByExchangeName(string exchangeName)
        {
            return _pharmacyRepo.GetPharmacyByExchangeName(exchangeName);
        }

        public PharmacySystem GetPharmacyById(int id)
        {
            return _pharmacyRepo.GetPharmacyById(id);
        }

        public void SubscribeUnsubscibe(int id, bool subscribe)
        {
            PharmacySystem p = _pharmacyRepo.GetPharmacyById(id);
            if (p == null)
                return;
            p.ActionsBenefitsSubscribed = subscribe;
            UpdatePharmacy(p);
        }

        public void UpdatePharmacy(PharmacySystem p)
        {
            _pharmacyRepo.UpdatePharmacy(p);
        }
    }
}
