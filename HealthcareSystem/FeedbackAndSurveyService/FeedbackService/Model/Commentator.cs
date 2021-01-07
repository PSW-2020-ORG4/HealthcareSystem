using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Model
{
    public class Commentator
    {
        public Jmbg Jmbg { get; }
        public string Name { get; }
        public string Surname { get; }

        public Commentator(string jmbg, string name, string surname)
        {
            Jmbg = new Jmbg(jmbg);
            Name = name;
            Surname = surname;
            Validate();
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Commentator name cannot be empty.");
            if (String.IsNullOrWhiteSpace(Surname))
                throw new ValidationException("Commentator surname cannot be empty.");
        }
    }
}
