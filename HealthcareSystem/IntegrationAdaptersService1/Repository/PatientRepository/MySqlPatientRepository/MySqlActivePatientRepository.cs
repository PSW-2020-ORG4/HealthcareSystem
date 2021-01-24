using Backend.Model;
using Backend.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersService1.Repository
{
    public class MySqlActivePatientRepository : IActivePatientRepository
    {
        private readonly MyDbContext _context;
        public MySqlActivePatientRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }
    }
}
