using Backend.Model.Pharmacies;
using Model.Manager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface IDrugService
    {
        public Task<List<Drug>> GetAll();
        public Task<bool> AddQuantity(string code, int quantity);
        public Task<List<DrugConsumptionReport>> GetDrugConsuption(DateRange dateRange);
    }
}
