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
        IEnumerable<Pharmacy> GetAllPharmacies();
        Pharmacy GetPharmacyById(int id);
        void CreatePharmacy(Pharmacy p);
        void UpdatePharmacy(Pharmacy p);
        void DeletePharmacy(Pharmacy p);
    }
}
