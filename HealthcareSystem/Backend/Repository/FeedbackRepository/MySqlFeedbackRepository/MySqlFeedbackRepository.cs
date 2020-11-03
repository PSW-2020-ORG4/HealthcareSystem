using Backend.Model;
using Backend.Model.Exceptions;
using Model.Users;
using MySql.Data.MySqlClient;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    class MySqlFeedbackRepository : IFeedbackRepository
    {
        private readonly MyDbContext _context;
        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.Find(id);
        }

        public List<Feedback> GetPublishedFeedbacks()
        {
            return _context.Feedbacks.Where(f => f.IsPublished == true).ToList();
        }

        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _context.Feedbacks.Where(f => f.IsPublished == false).ToList();

        }

        public void PublishFeedback(Feedback feedback)
        {
            _context.Update(feedback);
            _context.SaveChanges();
        }
    }
}
