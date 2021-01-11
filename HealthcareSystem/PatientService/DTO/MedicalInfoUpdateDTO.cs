using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.DTO
{
    public class MedicalInfoUpdateDTO
    {
        public BloodType BloodType { get; set; }
        public RhFactor RhFactor { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string InsuranceNumber { get; set; }
    }
}
