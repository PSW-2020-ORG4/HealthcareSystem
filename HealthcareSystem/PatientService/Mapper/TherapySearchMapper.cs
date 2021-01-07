using PatientService.DTO;
using PatientService.Model;
using PatientService.Model.Specification;
using PatientService.Model.TherapySpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Mapper
{
    public static class TherapySearchMapper
    {
        public static ISpecification<Therapy> ToTherapySpecification(this TherapySearchDTO parameters)
        {
            ISpecification<Therapy> filter = new TherapyStartDateSpecification(parameters.StartDate);
            filter = filter.BinaryOperation(
                parameters.EndDateOperator, new TherapyEndDateSpecification(parameters.EndDate));
            filter = filter.BinaryOperation(
                parameters.DoctorSurnameOperator, new TherapyDoctorSurnameSpecification(parameters.DoctorSurname));
            filter = filter.BinaryOperation(
                parameters.DrugNameOperator, new TherapyDrugNameSpacification(parameters.DrugName));

            return filter;
        }
    }
}
