﻿using Backend.Model.DTO;
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

        public void ScheduleAnAppointmentByDoctor(Examination scheduleExamination)
        {
            _examinationRepository.AddExamination(scheduleExamination);
        }
    }
}
