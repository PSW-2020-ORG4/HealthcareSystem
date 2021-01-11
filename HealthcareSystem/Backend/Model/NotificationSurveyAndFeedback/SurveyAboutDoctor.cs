using Backend.Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class SurveyAboutDoctor
    {
        private int examination;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BehaviorOfDoctor { get; set; }
        public int DoctorProfessionalism { get; set; }
        public int GettingAdviceByDoctor { get; set; }
        public int AvailabilityOfDoctor { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public virtual Examination Examination { get; set; }

        public SurveyAboutDoctor() { }

        public SurveyAboutDoctor(int behaviorOfDoctor, int doctorProfessionalism, int gettingAdviceByDoctor,
                                    int availabilityOfDoctor, int examinationId)
        {
            BehaviorOfDoctor = behaviorOfDoctor;
            DoctorProfessionalism = doctorProfessionalism;
            GettingAdviceByDoctor = gettingAdviceByDoctor;
            AvailabilityOfDoctor = availabilityOfDoctor;
            ExaminationId = examinationId;
        }

        public SurveyAboutDoctor(int behaviorOfDoctor, int doctorProfessionalism, int gettingAdviceByDoctor,
                                    int availabilityOfDoctor, Examination examination)
        {
            BehaviorOfDoctor = behaviorOfDoctor;
            DoctorProfessionalism = doctorProfessionalism;
            GettingAdviceByDoctor = gettingAdviceByDoctor;
            AvailabilityOfDoctor = availabilityOfDoctor;
            Examination = examination;
        }
    }
}
