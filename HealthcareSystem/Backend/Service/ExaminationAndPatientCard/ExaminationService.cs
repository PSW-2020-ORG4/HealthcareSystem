/***********************************************************************
 * Module:  ExaminationService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.ExaminationService
 ***********************************************************************/

using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Model.PerformingExamination;
using Backend.Repository.ExaminationRepository;
using Backend.Service.ExaminationAndPatientCard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ExaminationAndPatientCard
{
    public class ExaminationService : IExaminationService
    {
        private IExaminationRepository _scheduledExaminationRepository;

        public ExaminationService(IExaminationRepository scheduledExaminationRepository)
        {
            _scheduledExaminationRepository = scheduledExaminationRepository;
        }
        public void AddExamination(Examination examination)
        {
            _scheduledExaminationRepository.AddExamination(examination);
        }

        public List<Examination> GetAllExaminations()
        {
            return _scheduledExaminationRepository.GetAllExaminations();
        }

        public Examination GetExaminationById(int id)
        {
            return _scheduledExaminationRepository.GetExaminationById(id);
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            return _scheduledExaminationRepository.GetExaminationsByDate(date);
        }

        public List<Examination> GetExaminationsByPatient(string patientJmbg)
        {
            List<Examination> examinations = _scheduledExaminationRepository.GetExaminationsByPatient(patientJmbg);

            if (examinations == null)
            {
                throw new NotFoundException("There is no patients examinations in database.");
            }
            return examinations;
        }

        public void CancelExamination(int id)
        {
            Examination examination = GetExaminationById(id);
            examination.ExaminationStatus = ExaminationStatus.CANCELED;
            _scheduledExaminationRepository.UpdateExamination(examination);
        }

        public List<Examination> GetCanceledExaminationsByPatient(string patientJmbg)
        {
            return _scheduledExaminationRepository.GetCanceledExaminationsByPatient(patientJmbg);
        }

        public List<Examination> GetPreviousExaminationsByPatient(string patientJmbg)
        {
            return _scheduledExaminationRepository.GetPreviousExaminationsByPatient(patientJmbg);
        }

        public List<Examination> GetFollowingExaminationsByPatient(string patientJmbg)
        {
            return _scheduledExaminationRepository.GetFollowingExaminationsByPatient(patientJmbg);

        }

        public ICollection<Examination> GetExaminationsForPeriod(DateTime startDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.GetExaminationsForPeriod(startDate, endDate);
        }

        public ICollection<Examination> GetExaminationsForPeriodAndRoom(DateTime startDate, DateTime endDate, int roomId)
        {
            return (ICollection<Examination>)GetExaminationsForPeriod(startDate, endDate).Where(x => x.IdRoom == roomId).ToList();
        }
    }
}