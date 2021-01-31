/***********************************************************************
 * Module:  Examination.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Doctor.Examination
 ***********************************************************************/

using Model.Manager;
using Model.Secretary;
using System;

namespace Model.Doctor
{
   public class Examination
   {

        public int IdExamination { get; set; }
        public TypeOfExamination Type  { get; set; }
        public DateTime DateAndTime { get; set; }
        public Model.Users.Doctor doctor { get; set; }
        public Room room { get; set; }
        public Model.Secretary.PatientCard patientCard { get; set; }

        public Examination() 
        {
        }

        public Examination(int id, TypeOfExamination typeOfExamination, DateTime dateAndTime, Model.Users.Doctor doctor, Room room, PatientCard patientCard)
        {
            this.IdExamination = id;
            this.Type = typeOfExamination;
            this.DateAndTime = dateAndTime;
            if (doctor != null)
            {
                this.doctor = new Model.Users.Doctor(doctor);
            }
            else
            {
                this.doctor = new Model.Users.Doctor();
            }
            if (room != null)
            {
                this.room = new Room(room);
            }
            else
            {
                this.room = new Room();
            }
            if(patientCard != null)
            {
                this.patientCard = new PatientCard(patientCard);
            }
            else
            {
                this.patientCard = new PatientCard();
            }
            
        }
        public Examination(Examination examination)
        {
            this.IdExamination = examination.IdExamination;
            this.Type = examination.Type;
            this.DateAndTime = examination.DateAndTime;
            this.doctor = new Users.Doctor(examination.doctor);
            this.room = new Room(examination.room);
            this.patientCard = new Secretary.PatientCard(examination.patientCard);
        }
   
   }
}