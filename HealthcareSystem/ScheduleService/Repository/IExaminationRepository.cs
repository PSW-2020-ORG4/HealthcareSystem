using ScheduleService.Model;
using System.Collections.Generic;

namespace ScheduleService.Repository
{
    public interface IExaminationRepository
    {
        Examination Get(int id);
        IEnumerable<Examination> GetByExaminationStatusAndPatient(ExaminationStatus examinationStatus, string patientId);
        void Update(Examination examination);
        void Add(Examination examination);
    }
}
