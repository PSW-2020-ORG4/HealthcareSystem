/***********************************************************************
 * Module:  TherapyController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Examination&Drug&PatientCard&TherapyController.TherapyController
 ***********************************************************************/

using Model.Doctor;
using Service.DrugAndTherapy;
using System;
using System.Collections.Generic;

namespace Controller.DrugAndTherapy
{
    public class TherapyController
    {
        private TherapyService therapyService = new TherapyService();
		
		public int getLastId()
        {
            return therapyService.getLastId();
        }
		
        public Therapy AddTherapy(Therapy therapy)
        {
            return therapyService.AddTherapy(therapy);
        }

        public List<Therapy> ViewAllTherapyByPatient(string patientJmbg)
        {
            return therapyService.ViewAllTherapyByPatient(patientJmbg);
        }

        public Therapy EditTherapy(Therapy therapy)
        {
            return therapyService.EditTherapy(therapy);
        }

        public List<Therapy> ViewActiveTherapyByPatient(string patientJmbg)
        {
            return therapyService.ViewActiveTherapyByPatient(patientJmbg);
        }

        public List<Therapy> ViewTherapyForNextSevenDays(string patientJmbg)
        {
            return therapyService.ViewTherapyForNextSevenDays(patientJmbg);
        }

        public bool DeleteTherapy(int id)
        {
            return therapyService.DeleteTherapy(id);
        }

    }
}