/***********************************************************************
 * Module:  PatientCardController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.PatientCardController
 ***********************************************************************/

using Model.Users;
using Service.ExaminationAndPatientCard;
using System;

namespace Controller.ExaminationAndPatientCard
{
   public class PatientCardController
   {
        private PatientCardService _patientCardService;
        public void DeletePatientCard(string patientJmbg)
        {
            _patientCardService.DeletePatientCard(patientJmbg);
        }
        public PatientCard ViewPatientCard(string patientJmbg)
        {
            return _patientCardService.ViewPatientCard(patientJmbg);
        }  
        public void EditPatientCard(PatientCard patientCard)
        {
            _patientCardService.EditPatientCard(patientCard);
        }  
        public void CreatePatientCard(PatientCard patientCard)
        {
            _patientCardService.CreatePatientCard(patientCard);
        }  
   
   }
}