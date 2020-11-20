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
        private IPharmacyRepo _pharamcyRepo;

        public PharmacyService(IPharmacyRepo pharmacyRepo)
        {
            _pharamcyRepo = pharmacyRepo;
        }

        public void CreatePharmacy(Pharmacy p)
        {
            _pharamcyRepo.CreatePharmacy(p);
        }

        public void DeletePharmacy(Pharmacy p)
        {
            _pharamcyRepo.DeletePharmacy(p);
        }

        public IEnumerable<Pharmacy> GetAllPharmacies()
        {
            return _pharamcyRepo.GetAllPharmacies();
        }

        public Pharmacy GetPharmacyById(int id)
        {
            return _pharamcyRepo.GetPharmacyById(id);
        }

        public void UpdatePharmacy(Pharmacy p)
        {
            _pharamcyRepo.UpdatePharmacy(p);
        }
    }
}
