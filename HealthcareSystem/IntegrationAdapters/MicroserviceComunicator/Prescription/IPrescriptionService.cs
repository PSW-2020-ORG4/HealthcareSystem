using Backend.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface IPrescriptionService
    {
        public Task<List<Patient>> GetAllPatients();
    }
}
