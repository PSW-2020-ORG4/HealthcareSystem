/***********************************************************************
 * Module:  PatientCard.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Secretary.PatientCard
 ***********************************************************************/

using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Model.PerformingExamination;
using Backend.Model.Enums;

namespace Backend.Model.Users
{
    public class PatientCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public BloodType BloodType { get; set; }
        public RhFactorType RhFactor { get; set; }
        public string Alergies { get; set; }
        public string MedicalHistory { get; set; }
        public bool HasInsurance { get; set; }
        public string Lbo { get; set; }

        [ForeignKey("Patient")]
        public string PatientJmbg { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }

        public PatientCard() { }

        public PatientCard(int id, BloodType bloodType, RhFactorType rhFactor, string alergies,
                            string medicalHistory, bool hasInsurance, string lbo, string patientJmbg)
        {
            Id = id;
            BloodType = bloodType;
            RhFactor = rhFactor;
            Alergies = alergies;
            MedicalHistory = medicalHistory;
            HasInsurance = hasInsurance;
            Lbo = lbo;
            PatientJmbg = patientJmbg;
        }

        public PatientCard(PatientCard patientCard)
        {
            Id = patientCard.Id;
            BloodType = patientCard.BloodType;
            RhFactor = patientCard.RhFactor;
            Alergies = patientCard.Alergies;
            MedicalHistory = patientCard.MedicalHistory;
            HasInsurance = patientCard.HasInsurance;
            Lbo = patientCard.Lbo;
            PatientJmbg = patientCard.PatientJmbg;
        }
    }
}