/***********************************************************************
 * Module:  PatientCardController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.PatientCardController
 ***********************************************************************/

using Model.Secretary;
using Service.ExaminationAndPatientCard;
using System;

namespace Controller.ExaminationAndPatientCard
{
   public class PatientCardController
   {
        private PatientCardService patientCardService = new PatientCardService();

      public bool DeletePatientCard(string patientJmbg)
      {
            return patientCardService.DeletePatientCard(patientJmbg);
      }
      
      public PatientCard ViewPatientCard(string patientJmbg)
      {
            return patientCardService.ViewPatientCard(patientJmbg);
      }
      
      public PatientCard EditPatientCard(PatientCard patientCard)
      {
            return patientCardService.EditPatientCard(patientCard);
      }
      
      public PatientCard CreatePatientCard(PatientCard patientCard)
      {
            return patientCardService.CreatePatientCard(patientCard);
      }
   
   
   }
}