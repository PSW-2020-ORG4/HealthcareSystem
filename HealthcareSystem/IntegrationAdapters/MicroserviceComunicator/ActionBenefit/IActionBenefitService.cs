using Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface IActionBenefitService
    {
        public Task<ActionBenefit> Get(int id);
        public Task<List<ActionBenefit>> GetAll();
        public Task<bool> SetPublic(int id, bool isPublic);
    }
}
