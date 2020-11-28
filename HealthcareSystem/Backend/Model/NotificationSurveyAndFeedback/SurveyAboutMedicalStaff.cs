using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class SurveyAboutMedicalStaff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BehaviorOfMedicalStaff { get; set; }
        public int MedicalStaffProfessionalism { get; set; }
        public int GettingAdviceByMedicalStaff { get; set; }
        public int EaseInObtainingFollowUpInformation { get; set; }


        public SurveyAboutMedicalStaff() { }

        public SurveyAboutMedicalStaff(int behaviorOfMedicalStaff, int medicalStaffProfessionalism, 
                                       int gettingAdviceByMedicalStaff, int easeInObtainingFollowUpInformation)
        {
            BehaviorOfMedicalStaff = behaviorOfMedicalStaff;
            MedicalStaffProfessionalism = medicalStaffProfessionalism;
            GettingAdviceByMedicalStaff = gettingAdviceByMedicalStaff;
            EaseInObtainingFollowUpInformation = easeInObtainingFollowUpInformation;
        }
    }
}
