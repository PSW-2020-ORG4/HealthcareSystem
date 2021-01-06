using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyPermission
    {
        public int Id { get; }
        public Jmbg DoctorJmbg { get; }

        public SurveyPermission(int id, string doctorJmbg)
        {
            Id = id;
            DoctorJmbg = new Jmbg(doctorJmbg);
        }
    }
}
