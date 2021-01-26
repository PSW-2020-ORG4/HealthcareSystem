using ScheduleService.DTO;
using ScheduleService.Model;
using System.Collections.Generic;

namespace ScheduleService.Services
{
    public interface IExaminationService
    {
        Examination Get(int id);
        IEnumerable<Examination> GetFinishedByPatient(string jmbg);
        IEnumerable<Examination> GetCanceledByPatient(string jmbg);
        IEnumerable<Examination> GetCreatedByPatient(string jmbg);
        void Schedule(ScheduleExaminationDTO examinationDTO);
        void Cancel(int id);
    }
}
