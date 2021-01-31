using IntegrationAdapters.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationAdapters.MicroserviceComunicator
{
    public interface IPrescriptionService
    {
        public Task<List<PatientDto>> GetAllPatients();
    }
}
