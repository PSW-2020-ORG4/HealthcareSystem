/***********************************************************************
 * Module:  Examination.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Doctor.Examination
 ***********************************************************************/

using Backend.Model.Enums;
using Backend.Model.Users;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.PerformingExamination
{
    public class Examination
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TypeOfExamination Type { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Anamnesis { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Room")]
        public int IdRoom { get; set; }
        public virtual Room Room { get; set; }

        [ForeignKey("PatientCard")]
        public int IdPatientCard { get; set; }
        public virtual PatientCard PatientCard { get; set; }
        public ExaminationStatus ExaminationStatus { get; set; }
        public bool IsSurveyCompleted { get; set; }
        public virtual ICollection<Therapy> Therapies { get; set; }
        public virtual SurveyAboutDoctor SurveyAboutDoctor { get; set; }
        public virtual SurveyAboutMedicalStaff SurveyAboutMedicalStaff { get; set; }
        public virtual SurveyAboutHospital SurveyAboutHospital { get; set; }

        public Examination() { }

        public Examination(int id, TypeOfExamination typeOfExamination, DateTime dateAndTime, string anamnesis, Doctor doctor, Room room,
                            PatientCard patientCard, bool isSurveyCompleted = false, ExaminationStatus examinationStatus = ExaminationStatus.CREATED)
        {
            Id = id;
            Type = typeOfExamination;
            DateAndTime = dateAndTime;
            Anamnesis = anamnesis;
            if (doctor == null)
            {
                Doctor = new Doctor();
            }
            else
            {
                Doctor = new Doctor(doctor);
            }
            if (room == null)
            {
                Room = new Room();
            }
            else
            {
                Room = new Room(room);
            }
            if (patientCard == null)
            {
                PatientCard = new PatientCard();
            }
            else
            {
                PatientCard = new PatientCard(patientCard);
            }
            Therapies = new List<Therapy>();
            ExaminationStatus = examinationStatus;
            IsSurveyCompleted = isSurveyCompleted;
        }
        public Examination(Examination examination)
        {
            Id = examination.Id;
            Type = examination.Type;
            DateAndTime = examination.DateAndTime;
            Anamnesis = examination.Anamnesis;
            if (examination.Doctor == null)
            {
                Doctor = new Doctor();
            }
            else
            {
                Doctor = new Doctor(examination.Doctor);
            }
            if (examination.Room == null)
            {
                Room = new Room();
            }
            else
            {
                Room = new Room(examination.Room);
            }
            if (examination.PatientCard == null)
            {
                PatientCard = new PatientCard();
            }
            else
            {
                PatientCard = new PatientCard(examination.PatientCard);
            }
            Therapies = examination.Therapies;
            ExaminationStatus = ExaminationStatus.CREATED;
            IsSurveyCompleted = examination.IsSurveyCompleted;
        }

        public Examination(DateTime dateTime, string doctorJmbg, int roomId, int patientCardId)
        {
            DateAndTime = dateTime;
            DoctorJmbg = doctorJmbg;
            IdRoom = roomId;
            IdPatientCard = patientCardId;
            Type = TypeOfExamination.GENERAL;
            Anamnesis = null;
            ExaminationStatus = ExaminationStatus.CREATED;
            IsSurveyCompleted = false;
        }
        public Examination(DateTime dateTime, int roomId)
        {
            DateAndTime = dateTime;
            IdRoom = roomId;
            Type = TypeOfExamination.GENERAL;
            Anamnesis = null;
            ExaminationStatus = ExaminationStatus.CREATED;
            IsSurveyCompleted = false;
            IdPatientCard = 1;
            DoctorJmbg = "1234567891234";

        }

    }
}