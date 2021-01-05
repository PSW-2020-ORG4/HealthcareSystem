using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Model
{
    public class Content
    {
        private DateTime DateOfCreation { get; }
        private string Comment { get; }

        public Content(DateTime dateOfCreation, string comment)
        {
            DateOfCreation = dateOfCreation;
            Comment = comment;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Content content &&
                   DateOfCreation == content.DateOfCreation &&
                   Comment == content.Comment;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateOfCreation, Comment);
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
