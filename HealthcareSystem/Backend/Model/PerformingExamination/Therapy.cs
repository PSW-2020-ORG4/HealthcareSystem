/***********************************************************************
 * Module:  Anamnesis.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Doctor.Anamnesis
 ***********************************************************************/

using Model.Manager;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.PerformingExamination
{
   public class Therapy
   {
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      public string Anamnesis { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public int DailyDose { get; set; }

      [ForeignKey("Drug")]
      public int IdDrug { get; set; }
      public virtual Drug Drug { get; set; }

      [ForeignKey("Examination")]
      public int IdExamination { get; set; }
      public virtual Examination Examination { get; set; }

        public Therapy() { }

        public Therapy(Drug drug, Examination examination, int id, string anamnesis, DateTime startDate, DateTime endDate, int dailyDose)
        {
            if (drug == null)
            {
                Drug = new Drug();
            }
            else
            {
                Drug = new Drug(drug);
            }
            if (examination == null)
            {
                Examination = new Examination();
            }
            else
            {
                Examination = new Examination(examination);
            }
            Id = id;
            Anamnesis = anamnesis;
            StartDate = startDate;
            EndDate = endDate;
            DailyDose = dailyDose;
        }

        public Therapy(Therapy therapy)
        {
            if (therapy.Drug == null)
            {
                Drug = new Drug();
            }
            else
            {
                Drug = new Drug(therapy.Drug);
            }
            if (therapy.Examination == null)
            {
                Examination = new Examination();
            }
            else
            {
                Examination = new Examination(therapy.Examination);
            }
            Id = therapy.Id;
            Anamnesis = therapy.Anamnesis;
            StartDate = therapy.StartDate;
            EndDate = therapy.EndDate;
            DailyDose = therapy.DailyDose;
        }
    }
}