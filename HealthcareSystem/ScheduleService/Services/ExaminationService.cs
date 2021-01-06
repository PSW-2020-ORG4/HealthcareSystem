using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public class ExaminationService : IExaminationService
    {
        public void Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetCanceledByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetCreatedByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetFinishedByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        public void Schedule()
        {
            throw new NotImplementedException();
        }
    }
}
