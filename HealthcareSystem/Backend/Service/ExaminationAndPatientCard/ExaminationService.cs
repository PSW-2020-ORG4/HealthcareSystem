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
   public class ExaminationService: IExaminationService
   {
        private IExaminationRepository _scheduledExaminationRepository;

        public ExaminationService(IExaminationRepository scheduledExaminationRepository) {
            _scheduledExaminationRepository = scheduledExaminationRepository;
        }
        public void AddExamination(Examination examination)
        {
            _scheduledExaminationRepository.AddExamination(examination);
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.AppointmentRecommendationByDate(doctor, beginDate, endDate);
        }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.AppointmentRecommendationByDoctor(doctor, beginDate, endDate);
        }

        public bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.CheckDoctorAvailability(doctor, dateAndTime);
        }

        public bool CheckRoomAvailability(Room room, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.CheckRoomAvailability(room, dateAndTime);
        }

        public void DeleteDoctorScheduledExaminations(string doctorJmbg)
        {
             _scheduledExaminationRepository.DeleteDoctorScheduledExaminations(doctorJmbg);
        }

        public void DeleteExamination(int id)
        {
            _scheduledExaminationRepository.DeleteExamination(id);
        }

        public void DeletePatientScheduledExaminations(string patientJmbg)
        {
            _scheduledExaminationRepository.DeletePatientScheduledExaminations(patientJmbg);
        }

        public void DeleteRoomScheduledExaminations(int numberOfRoom)
        {
            _scheduledExaminationRepository.DeleteRoomScheduledExaminations(numberOfRoom);
        }

        public List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.fillAppointments(doctor, beginDate, endDate);
        }

        public List<Examination> GetAllExaminations()
        {
            return _scheduledExaminationRepository.GetAllExaminations();
        }

        public Examination GetExaminationByDoctorDateAndTime(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.GetExaminationByDoctorDateAndTime(doctor, dateAndTime);
        }

        public Examination GetExaminationById(int id)
        {
            return _scheduledExaminationRepository.GetExaminationById(id);
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            return _scheduledExaminationRepository.GetExaminationsByDate(date);
        }

        public List<Examination> GetExaminationsByDoctor(string doctorJmbg)
        {
            return _scheduledExaminationRepository.GetExaminationsByDoctor(doctorJmbg);
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.GetExaminationsByDoctorAndDate(doctor, dateAndTime);
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

        public List<Examination> GetExaminationsByRoom(int numberOfRoom)
        {
            return _scheduledExaminationRepository.GetExaminationsByRoom(numberOfRoom);
        }

        public List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.GetExaminationsByRoomAndDates(numberOfRoom, beginDate, endDate);
        }

        public List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.getFreeAppointments(doctor, beginDate, endDate);
        }

        public void UpdateExamination(Examination examination)
        {
            _scheduledExaminationRepository.UpdateExamination(examination);
        }

        public List<Examination> AdvancedSearch(ExaminationSearchDTO parameters)
        {
            List<Examination> examinations = GetExaminationsByPatient(parameters.Jmbg);

            ISpecification<Examination> filter = new ExaminationStartDateSpecification(parameters.StartDate);
            filter = filter.BinaryOperation(parameters.EndDateOperator, new ExaminationEndDateSpecification(parameters.EndDate));
            filter = filter.BinaryOperation(parameters.DoctorSurnameOperator, new ExaminationDoctorSurnameSpecification(parameters.DoctorSurname));
            filter = filter.BinaryOperation(parameters.AnamnesisOperator, new ExaminationAnamnesisSpecification(parameters.Anamnesis));
            
            return examinations.Where(examination => filter.IsSatisfiedBy(examination)).ToList();
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