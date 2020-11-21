/***********************************************************************
 * Module:  PatientCardService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.UsersService.PatientCardService
 ***********************************************************************/

using Model.Secretary;
using Repository;
using System;

namespace Service.ExaminationAndPatientCard
{
   public class PatientCardService
   {
        public ActivePatientCardRepository activePatientCardRepository = new ActivePatientCardRepository();
        public DeletedPatientCardRepository deletedPatientCardRepository = new DeletedPatientCardRepository();
      public bool DeletePatientCard(string patientJmbg)
      {
            PatientCard deletedPatientCard = activePatientCardRepository.DeletePatientCard(patientJmbg);
            if(deletedPatientCard != null)
            {
                PatientCard newPatientCard = deletedPatientCardRepository.NewPatientCard(deletedPatientCard);
                if(newPatientCard != null)
                {
                    return true;
                }
            }
            return false;
      }
      
      public PatientCard ViewPatientCard(string patientJmbg)
      {
            return activePatientCardRepository.GetPatientCard(patientJmbg);
      }
      
      public PatientCard EditPatientCard(PatientCard patientCard)
      {
            return activePatientCardRepository.SetPatientCard(patientCard);
      }
      
      public PatientCard CreatePatientCard(PatientCard patientCard)
      {
            return activePatientCardRepository.NewPatientCard(patientCard);
      }
   
   }
}