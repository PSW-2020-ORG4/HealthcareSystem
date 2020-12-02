/***********************************************************************
 * Module:  Examination.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Doctor.Examination
 ***********************************************************************/

using Model.Manager;
using Model.Users;
using Model.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Backend.Model;

namespace Model.PerformingExamination
{
   public class Examination
   {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TypeOfExamination Type  { get; set; }
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
        public virtual ICollection<Therapy> Therapies { get; set; }

        public virtual ICollection<SurveyAboutDoctor> SurveysAboutDoctor { get; set; }
        public virtual ICollection<SurveyAboutMedicalStaff> SurveysAboutMedicalStaff { get; set; }
        public virtual ICollection<SurveyAboutHospital> SurveysAboutHospital { get; set; }

        public Examination() 
        {
        }

        public Examination(int id, TypeOfExamination typeOfExamination, DateTime dateAndTime, string anamnesis, Doctor doctor, Room room, PatientCard patientCard)
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
        }
   
   }
}