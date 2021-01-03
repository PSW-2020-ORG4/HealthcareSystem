using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    public class PatientRepository : IPatientRepository
    {
        public void Add(PatientAccount entity)
        {
            throw new NotImplementedException();
        }

        public PatientAccount Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PatientAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PatientAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
