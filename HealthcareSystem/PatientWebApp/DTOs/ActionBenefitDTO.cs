using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class ActionBenefitDTO
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public ActionBenefitDTO() { }
    }
}
