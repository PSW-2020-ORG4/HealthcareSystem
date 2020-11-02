using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    class MySqlFeedbackRepository : IFeedbackRepository
    {
        public void AddFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetPublishedFeedbacks()
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetUnpublishedFeedbacks()
        {
            throw new NotImplementedException();
        }

        public void PublishFeedback(int id)
        {
            throw new NotImplementedException();
        }
    }
}
