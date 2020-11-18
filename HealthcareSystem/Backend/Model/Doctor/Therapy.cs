/***********************************************************************
 * Module:  Anamnesis.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Doctor.Anamnesis
 ***********************************************************************/

using Model.Manager;
using Model.Users;
using System;

namespace Model.Doctor
{
   public class Therapy
   {
      public Model.Manager.Drug drug { get; set; }
      public PatientCard patientCard { get; set; }
      public int Id { get; set; }
      public string Anamnesis { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public int DailyDose { get; set; }

        public Therapy() { }

        public Therapy(Drug drug, PatientCard patientCard, int id, string anamnesis, DateTime startDate, DateTime endDate, int dailyDose)
        {
            if (drug == null)
            {
                this.drug = new Drug();
            }
            else
            {
                this.drug = new Drug(drug);
            }
            if (patientCard == null)
            {
                this.patientCard = new PatientCard();
            }
            else
            {
                this.patientCard = new PatientCard(patientCard);
            }
            this.Id = id;
            this.Anamnesis = anamnesis;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DailyDose = dailyDose;
        }

        public Therapy(Therapy therapy)
        {
            if (therapy.drug == null)
            {
                this.drug = new Drug();
            }
            else
            {
                this.drug = new Drug(therapy.drug);
            }
            if (therapy.patientCard == null)
            {
                this.patientCard = new PatientCard();
            }
            else
            {
                this.patientCard = new PatientCard(therapy.patientCard);
            }
            this.Id = therapy.Id;
            this.Anamnesis = therapy.Anamnesis;
            this.StartDate = therapy.StartDate;
            this.EndDate = therapy.EndDate;
            this.DailyDose = therapy.DailyDose;
        }
    }
}