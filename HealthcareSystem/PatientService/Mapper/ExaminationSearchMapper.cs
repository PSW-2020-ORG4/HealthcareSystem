using PatientService.DTO;
using PatientService.Model;
using PatientService.Model.ExaminationSpecification;
using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Mapper
{
    public static class ExaminationSearchMapper
    {
        public static ISpecification<Examination> ToExaminationSpecification(this ExaminationSearchDTO parameters)
        {
            ISpecification<Examination> filter = new ExaminationStartDateSpecification(parameters.StartDate);
            filter = filter.BinaryOperation(
                parameters.EndDateOperator, new ExaminationEndDateSpecification(parameters.EndDate));
            filter = filter.BinaryOperation(
                parameters.DoctorSurnameOperator, new ExaminationDoctorSurnameSpecification(parameters.DoctorSurname));
            filter = filter.BinaryOperation(
                parameters.AnamnesisOperator, new ExaminationAnamnesisSpecification(parameters.Anamnesis));

            return filter;
        }
    }
}
