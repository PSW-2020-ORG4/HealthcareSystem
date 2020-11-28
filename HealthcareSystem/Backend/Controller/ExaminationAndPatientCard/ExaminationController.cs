/***********************************************************************
 * Module:  ExaminationController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.ExaminationController
 ***********************************************************************/

using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
using Service.ExaminationAndPatientCard;
using System;
using System.Collections.Generic;

namespace Controller.ExaminationAndPatientCard
{
   public class ExaminationController
   {
        private ExaminationService _examinationService;
      public void ScheduleExamination(Examination examination)
      {
            _examinationService.AddExamination(examination);
      }
      
      public void UpdateExamination(Examination examination)
      {
            _examinationService.UpdateExamination(examination);
      }
      
      public bool CancelExamination(int id)
      {
            throw new NotImplementedException();
        }

      public void DeleteScheduledExamination(int id)
      {
            _examinationService.DeleteExamination(id);
      }

      public void DeleteCanceledExamination(int id)
        {
            throw new NotImplementedException();
        }

       public void DeletePatientExaminations(string patientJmbg)
      {
            _examinationService.DeletePatientScheduledExaminations(patientJmbg);
      }

      public void DeleteDoctorExaminations(string doctorJmbg)
        {
            _examinationService.DeleteDoctorScheduledExaminations(doctorJmbg);
        }

      public void DeleteRoomExaminations(int numberOfRoom)
        {
            _examinationService.DeleteRoomScheduledExaminations(numberOfRoom);
        }

        public List<Examination> GetScheduledExaminationsByDate(DateTime date)
      {
            return _examinationService.GetExaminationsByDate(date);
      }
      
      public Examination GetScheduledExaminationById(int id)
      {
            return _examinationService.GetExaminationById(id);
      }
      
      public List<Examination> GetScheduledExaminations()
      {
           return _examinationService.GetAllExaminations();
      }

        public List<Examination> GetCanceledExaminations()
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime date)
        {
            return _examinationService.GetExaminationsByDoctorAndDate(doctor, date);
        }
      
      public List<Examination> GetExaminationsByPatient(string patientJmbg)
      {
            return _examinationService.GetExaminationsByPatient(patientJmbg);
      }

      public List<Examination> GetExaminationsByDoctor(string doctorJmbg)
        {
            return _examinationService.GetExaminationsByDoctor(doctorJmbg);
        }

        public List<Examination> GetExaminationsByRoom(int numberOfRoom)
        {
            return _examinationService.GetExaminationsByRoom(numberOfRoom);
        }
      
        public List<Examination> getAllAppointments(Doctor doctor,DateTime dateTime)
        {
            throw new NotImplementedException();
        }
        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _examinationService.AppointmentRecommendationByDoctor(doctor, beginDate, endDate);
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _examinationService.AppointmentRecommendationByDate(doctor, beginDate, endDate);
        }
    }
}