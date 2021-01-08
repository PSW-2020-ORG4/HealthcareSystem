using PatientService.DTO;
using PatientService.Mapper;
using PatientService.Model;
using PatientService.Model.Specification;
using PatientService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public Patient Get(string jmbg)
        {
            return _repository.GetLazy(jmbg);
        }

        public IEnumerable<Examination> GetExaminations(string jmbg)
        {
            Patient patient = _repository.Get(jmbg);
            return patient.Examinations;
        }

        public IEnumerable<Examination> GetExaminations(string jmbg, ExaminationSearchDTO parameters)
        {
            Patient patient = _repository.Get(jmbg);
            ISpecification<Examination> specification = parameters.ToExaminationSpecification();
            return patient.SearchExaminations(specification);
        }

        public IEnumerable<Therapy> GetTherapies(string jmbg)
        {
            Patient patient = _repository.Get(jmbg);
            return patient.Therapies;
        }

        public IEnumerable<Therapy> GetTherapies(string jmbg, TherapySearchDTO parameters)
        {
            Patient patient = _repository.Get(jmbg);
            ISpecification<Therapy> specification = parameters.ToTherapySpecification();
            return patient.SearchTherapies(specification);
        }
    }
}
