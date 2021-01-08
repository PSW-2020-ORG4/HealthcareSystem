using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Model.Memento
{
    public interface IOriginator<T> where T : IMemento
    {
        T GetMemento();
    }
}
