using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.NotificationSurveyAndFeedback
{
    public interface IFeedbackService
    {
        void AddFeedback(Feedback feedback);
        Feedback GetFeedbackById(int id);
        void PublishFeedback(int id);
        List<Feedback> GetPublishedFeedbacks();
        List<Feedback> GetUnpublishedFeedbacks();
    }
}
