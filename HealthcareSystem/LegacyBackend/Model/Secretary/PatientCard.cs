/***********************************************************************
 * Module:  PatientCard.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Model.Secretary.PatientCard
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Model.Secretary
{
    public class PatientCard
    {

        public Model.Users.Patient patient;
        public BloodType BloodType;
        public RhFactorType RhFactor;
        public string Alergies;
        public string MedicalHistory;
        public bool HasInsurance;
        public string Lbo;
        public List<Doctor.Examination> examinationList { get; set; }

        public PatientCard() { }

        public PatientCard(Model.Users.Patient patient, BloodType bloodType, RhFactorType rhFactor, string alergies, string medicalHistory, bool hasInsurance, string lbo)
        {
            if (patient == null)
            {
                this.patient = new Users.Patient();
            }
            else
            {
                this.patient = new Users.Patient(patient);
            }
            this.BloodType = bloodType;
            this.RhFactor = rhFactor;
            this.Alergies = alergies;
            this.MedicalHistory = medicalHistory;
            this.HasInsurance = hasInsurance;
            this.Lbo = lbo;
            this.examinationList = new List<Doctor.Examination>();
        }

        public PatientCard(Model.Users.Patient patient)
        {
            if (patient == null)
            {
                this.patient = new Users.Patient();
            }
            else
            {
                this.patient = new Users.Patient(patient);
            }
            this.examinationList = new List<Doctor.Examination>();
        }

        public PatientCard(PatientCard patientCard)
        {
            if (patientCard.patient == null)
            {
                this.patient = new Users.Patient();
            }
            else
            {
                this.patient = new Users.Patient(patientCard.patient);
            }
            this.BloodType = patientCard.BloodType;
            this.RhFactor = patientCard.RhFactor;
            this.Alergies = patientCard.Alergies;
            this.MedicalHistory = patientCard.MedicalHistory;
            this.HasInsurance = patientCard.HasInsurance;
            this.Lbo = patientCard.Lbo;
            this.examinationList = patientCard.examinationList;
        }
    }
}