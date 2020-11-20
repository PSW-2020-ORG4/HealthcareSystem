/***********************************************************************
 * Module:  ExaminationController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.ExaminationController
 ***********************************************************************/

using Model.Doctor;
using Model.Manager;
using Model.Users;
using Service.ExaminationAndPatientCard;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;

namespace Controller.ExaminationAndPatientCard
{
    public class ExaminationController
    {
        private ExaminationService examinationService = new ExaminationService();
        public Examination ScheduleExamination(Examination examination)
        {
            return examinationService.ScheduleExamination(examination);
        }

        public Examination EditExamination(Examination examination)
        {
            return examinationService.EditExamination(examination);
        }

        public bool CancelExamination(int id)
        {
            return examinationService.CancelExamination(id);
        }

        public bool DeleteScheduledExamination(int id)
        {
            return examinationService.DeleteScheduledExamination(id);
        }

        public bool DeleteCanceledExamination(int id)
        {
            return examinationService.DeleteCanceledExamination(id);
        }

        public bool DeletePatientExaminations(string patientJmbg)
        {
            return examinationService.DeletePatientExaminations(patientJmbg);
        }

        public bool DeleteDoctorExaminations(string doctorJmbg)
        {
            return examinationService.DeleteDoctorExaminations(doctorJmbg);
        }

        public bool DeleteRoomExaminations(int numberOfRoom)
        {
            return examinationService.DeleteRoomExaminations(numberOfRoom);
        }

        public List<Examination> ViewScheduledExaminationsByDate(DateTime date)
        {
            return examinationService.ViewScheduledExaminationsByDate(date);
        }

        public Examination ViewScheduledExaminationById(int id)
        {
            return examinationService.ViewScheduledExaminationById(id);
        }

        public List<Examination> ViewScheduledExaminations()
        {
            return examinationService.ViewScheduledExaminations();
        }

        public List<Examination> ViewCanceledExaminations()
        {
            return examinationService.ViewCanceledExaminations();
        }

        public List<Examination> ViewExaminationsByDoctorAndDate(Doctor doctor, DateTime date)
        {
            return examinationService.ViewExaminationsByDoctorAndDate(doctor, date);
        }

        public List<Examination> ViewExaminationsByPatient(string patientJmbg)
        {
            return examinationService.ViewExaminationsByPatient(patientJmbg);
        }

        public List<Examination> ViewExaminationsByDoctor(string doctorJmbg)
        {
            return examinationService.ViewExaminationsByDoctor(doctorJmbg);
        }

        public int getLastId()
        {
            return examinationService.getLastId();
        }

        public List<Examination> ViewExaminationsByRoom(int numberOfRoom)
        {
            return examinationService.ViewExaminationsByRoom(numberOfRoom);
        }

        public void SaveExaminationInPatientCard(Examination examination)
        {
            examinationService.SaveExaminationInPatientCard(examination);
        }

        public List<Examination> getAllAppointments(Doctor doctor, DateTime dateTime)
        {
            return examinationService.getAllAppointments(doctor, dateTime);
        }
        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return examinationService.AppointmentRecommendationByDoctor(doctor, beginDate, endDate);
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return examinationService.AppointmentRecommendationByDate(doctor, beginDate, endDate);
        }
    }
}