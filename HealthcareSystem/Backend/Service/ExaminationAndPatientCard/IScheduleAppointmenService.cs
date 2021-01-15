using Backend.Model.PerformingExamination;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IScheduleAppointmenService
    {
        int ScheduleAnAppointmentByDoctor(Examination scheduleExamination);
        void ReScheduleAppointment(Examination examinationForSchedule, Examination examinationForReschedule, Examination shiftedExamination);
        Examination GetExaminationById(int id); 
    }
}
