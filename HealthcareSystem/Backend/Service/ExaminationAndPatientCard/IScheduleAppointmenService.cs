using Backend.Model.PerformingExamination;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IScheduleAppointmenService
    {
        int ScheduleAnAppointmentByDoctor(Examination scheduleExamination);
        Examination GetExaminationById(int id); 
    }
}
