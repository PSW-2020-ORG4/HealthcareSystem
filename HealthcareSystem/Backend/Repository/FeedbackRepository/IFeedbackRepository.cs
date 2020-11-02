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
        void AddFeedback(Feedback feedback);
        void PublishFeedback(int id);
        List<Feedback> GetPublishedFeedbacks();
        List<Feedback> GetUnpublishedFeedbacks();
    }
}
