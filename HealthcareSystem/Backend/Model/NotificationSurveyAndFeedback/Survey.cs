using Backend.Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Survey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BehaviorOfDoctor { get; set; }
        public int DoctorProfessionalism { get; set; }
        public int GettingAdviceByDoctor { get; set; }
        public int AvailabilityOfDoctor { get; set; }

        public int BehaviorOfMedicalStaff { get; set; }
        public int MedicalStaffProfessionalism { get; set; }
        public int GettingAdviceByMedicalStaff { get; set; }
        public int EaseInObtainingFollowupInformationAndCare { get; set; }

        public int Nursing { get; set; }
        public int Cleanliness { get; set; }
        public int OverallRating { get; set; }
        public int SatisfiedWithDrugAndInstrument { get; set; }


        [ForeignKey("Doctor")]
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }

        public Survey() { }

    }
}
