using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IFeedbackRepository
    {
        Feedback GetFeedbackById(int id);
        void AddFeedback(Feedback feedback);
        void PublishFeedback(Feedback feedback);
        List<Feedback> GetPublishedFeedbacks();
        List<Feedback> GetUnpublishedFeedbacks();
    }
}
