using ScheduleService.DTO;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public interface IExaminationService
    {
        IEnumerable<Examination> GetFinishedByPatient(string jmbg);
        IEnumerable<Examination> GetCanceledByPatient(string jmbg);
        IEnumerable<Examination> GetCreatedByPatient(string jmbg);
        void Schedule(ExaminationDTO examinationDTO);
        void Cancel(int id);

    }
}
