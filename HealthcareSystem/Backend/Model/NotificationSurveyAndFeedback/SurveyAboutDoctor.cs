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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BehaviorOfDoctor { get; set; }
        public int DoctorProfessionalism { get; set; }
        public int GettingAdviceByDoctor { get; set; }
        public int AvailabilityOfDoctor { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }

        public SurveyAboutDoctor() { }

        public SurveyAboutDoctor(int behaviorOfDoctor, int doctorProfessionalism, int gettingAdviceByDoctor,
                                    int availabilityOfDoctor, string doctorJmbg)
        {
            BehaviorOfDoctor = behaviorOfDoctor;
            DoctorProfessionalism = doctorProfessionalism;
            GettingAdviceByDoctor = gettingAdviceByDoctor;
            AvailabilityOfDoctor = availabilityOfDoctor;
            DoctorJmbg = doctorJmbg;
        }
    }
}
