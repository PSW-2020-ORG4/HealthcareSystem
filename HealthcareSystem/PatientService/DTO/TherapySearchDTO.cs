﻿using PatientService.Model.Specification;
using System;

namespace PatientService.DTO
{
    public class TherapySearchDTO
    {
        public DateTime? StartDate { get; set; }
        public LogicalOperator EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public LogicalOperator DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public LogicalOperator DrugNameOperator { get; set; }
        public string DrugName { get; set; }

        public TherapySearchDTO()
        {
        }

        public TherapySearchDTO(DateTime startDate, LogicalOperator endOperator, DateTime endDate, LogicalOperator doctorOperator, string doctorSurname, LogicalOperator drugOperator, string drugName)
        {
            StartDate = startDate;
            EndDateOperator = endOperator;
            EndDate = endDate;
            DoctorSurnameOperator = doctorOperator;
            DoctorSurname = doctorSurname;
            DrugNameOperator = drugOperator;
            DrugName = drugName;
        }
    }

}
