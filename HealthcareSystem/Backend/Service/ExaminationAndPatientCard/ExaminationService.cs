/***********************************************************************
 * Module:  ExaminationService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.ExaminationService
 ***********************************************************************/

using Model.Doctor;
using Model.Manager;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Service.ExaminationAndPatientCard
{
   public class ExaminationService
   {
        private FileActivePatientCardRepository activePatientCardRepository = new FileActivePatientCardRepository();
        private CanceledExaminationRepository canceledExaminationRepository = new CanceledExaminationRepository();
        private ScheduledExaminationRepository scheduledExaminationRepository = new ScheduledExaminationRepository();

        public Examination ScheduleExamination(Examination examination)
      {
            bool doctorAvailability = CheckDoctorAvailability(examination.doctor,examination.DateAndTime);
            bool roomAvailability = CheckRoomAvailability(examination.room,examination.DateAndTime);

            if (!doctorAvailability || !roomAvailability)
                return null;

            if(canceledExaminationRepository.GetExaminationById(examination.IdExamination) != null)
            {
                canceledExaminationRepository.DeleteExamination(examination.IdExamination);
            }

            return scheduledExaminationRepository.NewExamination(examination);
      }
      
      public Examination EditExamination(Examination examination)
      {
            return scheduledExaminationRepository.SetExamination(examination);
      }

        public bool CancelExamination(int id)
        {
            Examination examinationForDelete = scheduledExaminationRepository.DeleteExamination(id);
            if (examinationForDelete != null)
            {
                Examination newExamination = canceledExaminationRepository.NewExamination(examinationForDelete);
                if (newExamination != null)
                {
                    return true;
                }
            }
            return false;
      }

        public bool DeleteScheduledExamination(int id)
        {
            if(scheduledExaminationRepository.DeleteExamination(id) != null)
            {
                return true;
            }
            return false;
        }

        public bool DeleteCanceledExamination(int id)
        {
            if (canceledExaminationRepository.DeleteExamination(id) != null)
            {
                return true;
            }
            return false;
        }

        public bool DeletePatientExaminations(string patientJmbg)
        {
            if (scheduledExaminationRepository.DeletePatientScheduledExaminations(patientJmbg))
            {
                return canceledExaminationRepository.DeletePatientCanceledExaminations(patientJmbg);
            }
            return false;
        }

        public int getLastId()
        {
            return scheduledExaminationRepository.getLastId();
        }

        public bool DeleteDoctorExaminations(string doctorJmbg)
        {
            if (scheduledExaminationRepository.DeleteDoctorScheduledExaminations(doctorJmbg))
            {
                return canceledExaminationRepository.DeleteDoctorCanceledExaminations(doctorJmbg);
            }
            return false;
        }

        public bool DeleteRoomExaminations(int numberOfRoom)
        {
            if (scheduledExaminationRepository.DeleteRoomScheduledExaminations(numberOfRoom))
            {
                return canceledExaminationRepository.DeleteRoomCanceledExaminations(numberOfRoom);
            }
            return false;
        }

        public List<Examination> getAllAppointments(Doctor doctor,DateTime dateAndTime)
        {
            List<Examination> allAppointments = scheduledExaminationRepository.fillAppointments(doctor,dateAndTime,dateAndTime);
            return allAppointments;
        }
      
      public List<Examination> ViewScheduledExaminationsByDate(DateTime date)
      {
            return scheduledExaminationRepository.GetExaminationsByDate(date);
      }
      
      public Examination ViewScheduledExaminationById(int id)
      {
            return scheduledExaminationRepository.GetExaminationById(id);
      }
      
      public List<Examination> ViewCanceledExaminations()
      {
            return canceledExaminationRepository.GetAllExaminations();
      }

        public List<Examination> ViewScheduledExaminations()
        {
            return scheduledExaminationRepository.getAllExaminations();
        }
      
      public List<Examination> ViewExaminationsByPatient(string patientJmbg)
      {
            return scheduledExaminationRepository.GetExaminationsByPatient(patientJmbg);
      }

      public List<Examination > ViewExaminationsByDoctor(string doctorJmbg)
        {
            return scheduledExaminationRepository.GetExaminationsByDoctor(doctorJmbg);
        }

        public List<Examination> ViewExaminationsByRoom(int numberOfRoom)
        {
            return scheduledExaminationRepository.GetExaminationsByRoom(numberOfRoom);
        }

        public List<Examination> ViewExaminationsByDoctorAndDate(Doctor doctor, DateTime date)
        {
            return scheduledExaminationRepository.GetExaminationsByDoctorAndDate(doctor, date);
        }

        public void SaveExaminationInPatientCard(Examination examination)
      {
            activePatientCardRepository.SaveExaminationInPatientCard(examination);
      }
   
      private bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime)
      {
            return scheduledExaminationRepository.CheckDoctorAvailability(doctor, dateAndTime);
      }
      
      private bool CheckRoomAvailability(Room room, DateTime dateAndTime)
      {
            return scheduledExaminationRepository.CheckRoomAvailability(room, dateAndTime);
      }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return scheduledExaminationRepository.AppointmentRecommendationByDoctor(doctor, beginDate, endDate);
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return scheduledExaminationRepository.AppointmentRecommendationByDate(doctor, beginDate, endDate);
        }
    }
}