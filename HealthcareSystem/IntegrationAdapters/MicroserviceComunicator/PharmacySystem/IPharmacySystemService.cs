using Backend.Model.Pharmacies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface IPharmacySystemService
    {
        public Task<PharmacySystem> Get(int id);
        public Task<List<PharmacySystem>> GetAll();
        public Task<List<PharmacySystem>> GetBySubscribed(bool subscribed);
        public Task<PharmacySystem> Create(PharmacySystem pharmacySystem);
        public Task<bool> Update(PharmacySystem pharmacySystem);
    }
}
