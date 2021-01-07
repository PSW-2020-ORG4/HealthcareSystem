/***********************************************************************
 * Module:  ExaminationService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.ExaminationService
 ***********************************************************************/

using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using Backend.Repository.ExaminationRepository;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Model.Exceptions;
using Backend.Service.SearchSpecification;
using System.Linq;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Backend.Model.Enums;

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

        public List<Examination> AdvancedSearch(ExaminationSearchDTO parameters)
        {
            List<Examination> examinations = GetPreviousExaminationsByPatient("");

            ISpecification<Examination> filter = new ExaminationStartDateSpecification(parameters.StartDate);
            filter = filter.BinaryOperation(parameters.EndDateOperator, new ExaminationEndDateSpecification(parameters.EndDate));
            filter = filter.BinaryOperation(parameters.DoctorSurnameOperator, new ExaminationDoctorSurnameSpecification(parameters.DoctorSurname));
            filter = filter.BinaryOperation(parameters.AnamnesisOperator, new ExaminationAnamnesisSpecification(parameters.Anamnesis));

            return examinations.Where(examination => filter.IsSatisfiedBy(examination)).ToList();
        }

        public void CompleteSurveyAboutExamination(int id)
        {
            try
            {
                Examination examination = GetExaminationById(id);
                examination.IsSurveyCompleted = true;
                _scheduledExaminationRepository.UpdateExamination(examination);
            }
            catch (DatabaseException exception)
            {
                throw new DatabaseException(exception.Message);
            }
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
    }
}