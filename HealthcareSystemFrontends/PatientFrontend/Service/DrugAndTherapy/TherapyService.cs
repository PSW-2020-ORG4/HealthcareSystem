/***********************************************************************
 * Module:  TherapyService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.TherapyService
 ***********************************************************************/

using Model.Doctor;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.DrugAndTherapy
{
    public class TherapyService
    {
        private TherapyRepository therapyRepository = new TherapyRepository();
		
		public int getLastId()
        {
            return therapyRepository.getLastId();
        }

        public Therapy AddTherapy(Therapy therapy)
        {
            return therapyRepository.NewTherapy(therapy);
        }

        public List<Therapy> ViewAllTherapyByPatient(string patientJmbg)
        {
            return therapyRepository.GetTherapyByPatient(patientJmbg);
        }

        public Therapy EditTherapy(Therapy therapy)
        {
            return therapyRepository.SetTherapy(therapy);
        }

        public List<Therapy> ViewActiveTherapyByPatient(string patientJmbg)
        {
            return therapyRepository.GetActiveTherapyByPatient(patientJmbg);
        }

        public List<Therapy> ViewTherapyForNextSevenDays(string patientJmbg)
        {
            return therapyRepository.GetTherapyForNextSevenDays(patientJmbg);
        }

        public bool DeleteTherapy(int id)
        {
            return therapyRepository.DeleteTherapy(id);
        }

    }
}