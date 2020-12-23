using Model.PerformingExamination;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IScheduleAppointmenService
    {
        void ScheduleAnAppointmentByDoctor(Examination scheduleExamination);
        Examination GetExaminationById(int id); 
    }
}
