using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyPermission
    {
        private int Id { get; }
        private Jmbg DoctorJmbg { get; }

        public bool IsExistId(int id)
        {
            return Id == id;
        }
    }
}
