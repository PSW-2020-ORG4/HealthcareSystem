using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Pharmacies
{
    public class MockPharmacyService : IPharmacyService
    {
        public void CreatePharmacy(Pharmacy p)
        {
            throw new NotImplementedException();
        }

        public void DeletePharmacy(Pharmacy p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pharmacy> GetAllPharmacies()
        {
            return new[] { new Pharmacy { Id = 1, ApiKey = "api", Name = "name" } };
        }

        public Pharmacy GetPharmacyById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePharmacy(Pharmacy p)
        {
            throw new NotImplementedException();
        }
    }
}
