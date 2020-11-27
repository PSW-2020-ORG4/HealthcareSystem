/***********************************************************************
 * Module:  TherapyController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.TherapyController
 ***********************************************************************/

using Model.PerformingExamination;
using Service.DrugAndTherapy;
using System;
using System.Collections.Generic;

namespace Controller.DrugAndTherapy
{
    public class TherapyController
    {
        private TherapyService _therapyService;
		
        public void AddTherapy(Therapy therapy)
        {
           _therapyService.AddTherapy(therapy);
        }

        public List<Therapy> GetTherapyByPatient(string patientJmbg)
        {
            return _therapyService.GetTherapyByPatient(patientJmbg);
        }

        public void UpdateTherapy(Therapy therapy)
        {
            _therapyService.UpdateTherapy(therapy);
        }

        public List<Therapy> GetActiveTherapyByPatient(string patientJmbg)
        {
            return _therapyService.GetActiveTherapyByPatient(patientJmbg);
        }

        public List<Therapy> GetTherapyForNextSevenDaysByPatient(string patientJmbg)
        {
            return _therapyService.GetTherapyForNextSevenDaysByPatient(patientJmbg);
        }

        public void DeleteTherapy(int id)
        {
            _therapyService.DeleteTherapy(id);
        }

        public List<Therapy> GetTherapyByDrug(int idDrug)
        {
            return _therapyService.GetTherapyByDrug(idDrug);
        }


        public void DeleteDrugTherapies(int id)
        {
            _therapyService.DeleteDrugTherapies(id);
        }

    }
}