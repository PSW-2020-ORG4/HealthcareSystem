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

namespace Model.Users
{
    public class PatientCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public BloodType BloodType { get; set; }
        public RhFactorType RhFactor { get; set; }
        public string Alergies { get; set; }
        public string MedicalHistory { get; set; }
        public bool HasInsurance { get; set; }
        public string Lbo { get; set; }

        [ForeignKey("Patient")]
        public string PatientJmbg { get; set; }
        public virtual Patient Patient { get; set; }
        public ICollection<Examination> Examinations { get; set; }

        public PatientCard() { }

        public PatientCard(Patient patient, BloodType bloodType, RhFactorType rhFactor, string alergies, string medicalHistory, bool hasInsurance, string lbo)
        {
            if (patient == null)
            {
                Patient = new Patient();
            }
            else
            {
                Patient = new Patient(patient);
            }
            BloodType = bloodType;
            RhFactor = rhFactor;
            Alergies = alergies;
            MedicalHistory = medicalHistory;
            HasInsurance = hasInsurance;
            Lbo = lbo;
            Examinations = new List<Examination>();
        }

        public PatientCard(Patient patient)
        {
            if (patient == null)
            {
                Patient = new Patient();
            }
            else
            {
                Patient = new Patient(patient);
            }
            Examinations = new List<Examination>();
        }

        public PatientCard(PatientCard patientCard)
        {
            if (patientCard.Patient == null)
            {
                Patient = new Patient();
            }
            else
            {
                Patient = new Patient(patientCard.Patient);
            }
            BloodType = patientCard.BloodType;
            RhFactor = patientCard.RhFactor;
            Alergies = patientCard.Alergies;
            MedicalHistory = patientCard.MedicalHistory;
            HasInsurance = patientCard.HasInsurance;
            Lbo = patientCard.Lbo;
            Examinations = patientCard.Examinations;
        }
    }
}