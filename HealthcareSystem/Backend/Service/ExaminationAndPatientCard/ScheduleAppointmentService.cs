using Backend.Model.DTO;
using Backend.Model.PerformingExamination;
using Backend.Repository.ExaminationRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class ScheduleAppointmentService : IScheduleAppointmenService
    {
        private readonly IExaminationRepository _examinationRepository;
        public ScheduleAppointmentService(IExaminationRepository examinationRepository) {
            _examinationRepository = examinationRepository;
        }

        public Examination GetExaminationById(int id)
        {
            return _examinationRepository.GetExaminationById(id);
        }

        public void ReScheduleAppointment(Examination examinationForSchedule, Examination examinationForReschedule, Examination shiftedExamination)
        {
            _examinationRepository.ReScheduleAppointment(examinationForSchedule, examinationForReschedule, shiftedExamination);
        }

        public int ScheduleAnAppointmentByDoctor(Examination scheduleExamination)
        {
            return _examinationRepository.AddExamination(scheduleExamination);
        }
    }
}
