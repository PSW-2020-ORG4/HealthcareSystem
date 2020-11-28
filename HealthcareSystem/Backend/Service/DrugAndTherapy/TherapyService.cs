/***********************************************************************
 * Module:  TherapyService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.TherapyService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository.TherapyRepository;
using Backend.Service.DrugAndTherapy;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.TherapySearch;
using Model.PerformingExamination;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.DrugAndTherapy
{
    public class TherapyService : ITherapyService
    {
        private ITherapyRepository _therapyRepository;

       public TherapyService(ITherapyRepository therapyRepository) {
            _therapyRepository = therapyRepository;
       }

        public void UpdateTherapy(Therapy therapy)
        {
            _therapyRepository.UpdateTherapy(therapy);
        }

        public Therapy GetTherapyById(int id)
        {
            Therapy therapy = _therapyRepository.GetTherapyById(id);
            
            if (therapy == null)
                throw new NotFoundException("Therapy with id=" + id + " doesn't exist in database.");
            return therapy;
        }

        public void DeleteTherapy(int idTherapy)
        {
            _therapyRepository.DeleteTherapy(idTherapy);
        }

        public List<Therapy> GetTherapyByPatient(string patientJmbg)
        {
            List<Therapy> therapies = _therapyRepository.GetTherapyByPatient(patientJmbg);

            if (therapies == null)
            {
                throw new NotFoundException("There is no patients therapies in database.");
            }
            return therapies;            
        }

        public List<Therapy> GetTherapyByDrug(int idDrug)
        {
            return _therapyRepository.GetTherapyByDrug(idDrug);
        }

        public List<Therapy> GetActiveTherapyByPatient(string patientJmbg)
        {
            return _therapyRepository.GetActiveTherapyByPatient(patientJmbg);
        }

        public List<Therapy> GetTherapyForNextSevenDaysByPatient(string patientJmbg)
        {
            return _therapyRepository.GetTherapyForNextSevenDaysByPatient(patientJmbg);
        }

        void ITherapyService.DeletePatientTherapies(string patientJmbg)
        {
            _therapyRepository.DeletePatientTherapies(patientJmbg);
        }

        public void DeleteDrugTherapies(int id)
        {
            _therapyRepository.DeleteDrugTherapies(id);
        }

        public void AddTherapy(Therapy therapy)
        {
            _therapyRepository.AddTherapy(therapy);
        }

        public List<Therapy> AdvancedSearch(TherapySearchDTO parameters)
        {
            List<Therapy> therapies = GetTherapyByPatient(parameters.Jmbg);

            ISpecification<Therapy> filter = new TherapyStartDateSpecification(parameters.StartDate);
            filter = filter.BinaryOperation(parameters.EndDateOperator, new TherapyEndDateSpecification(parameters.EndDate));
            filter = filter.BinaryOperation(parameters.DoctorSurnameOperator, new TherapyDoctorSurnameSpecification(parameters.DoctorSurname));
            filter = filter.BinaryOperation(parameters.DrugNameOperator, new TherapyDrugNameSpacification(parameters.DrugName));

            return therapies.Where(therapy => filter.IsSatisfiedBy(therapy)).ToList();
        }
    }
}