using Backend.Model.PerformingExamination;
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
        private Examination examination;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BehaviorOfMedicalStaff { get; set; }
        public int MedicalStaffProfessionalism { get; set; }
        public int GettingAdviceByMedicalStaff { get; set; }
        public int EaseInObtainingFollowUpInformation { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public virtual Examination Examination { get; set; }

        public SurveyAboutMedicalStaff() { }

        public SurveyAboutMedicalStaff(int behaviorOfMedicalStaff, int medicalStaffProfessionalism, int gettingAdviceByMedicalStaff, 
                                        int easeInObtainingFollowUpInformation, int examinationId)
        {
            BehaviorOfMedicalStaff = behaviorOfMedicalStaff;
            MedicalStaffProfessionalism = medicalStaffProfessionalism;
            GettingAdviceByMedicalStaff = gettingAdviceByMedicalStaff;
            EaseInObtainingFollowUpInformation = easeInObtainingFollowUpInformation;
            ExaminationId = examinationId;
        }

        public SurveyAboutMedicalStaff(int behaviorOfMedicalStaff, int medicalStaffProfessionalism, int gettingAdviceByMedicalStaff, 
                                        int easeInObtainingFollowUpInformation, Examination examination)
        {
            BehaviorOfMedicalStaff = behaviorOfMedicalStaff;
            MedicalStaffProfessionalism = medicalStaffProfessionalism;
            GettingAdviceByMedicalStaff = gettingAdviceByMedicalStaff;
            EaseInObtainingFollowUpInformation = easeInObtainingFollowUpInformation;
            Examination = examination;
        }
    }
}
