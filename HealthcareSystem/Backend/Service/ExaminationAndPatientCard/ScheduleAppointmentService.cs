using Backend.Model.DTO;
using Backend.Repository.ExaminationRepository;
using Model.PerformingExamination;
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
        public void ScheduleAnAppointmentByDoctor(Examination scheduleExamination)
        {
            _examinationRepository.AddExamination(scheduleExamination);
        }
    }
}
