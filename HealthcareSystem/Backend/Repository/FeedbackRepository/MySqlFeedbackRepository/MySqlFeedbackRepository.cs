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
    public class MySqlFeedbackRepository : IFeedbackRepository
    {
        private readonly MyDbContext _context;
        public MySqlFeedbackRepository(MyDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// /adding new feedback to database
        /// </summary>
        /// <param name="feedback">an object to be added to the database</param>
        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
        /// <summary>
        /// /getting feedback by id from database
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>object type Feedback</returns>
        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.Find(id);
        }
        /// <summary>
        /// /getting all published feedbacks from database
        /// </summary>
        /// <returns>list of published feedbacks</returns>
        public List<Feedback> GetPublishedFeedbacks()
        {
            return _context.Feedbacks.Where(f => f.IsPublished == true).ToList();
        }
        /// <summary>
        /// /getting all unpublished feedbacks from database
        /// </summary>
        /// <returns>list of unpublished feedbacks</returns>
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _context.Feedbacks.Where(f => f.IsPublished == false).ToList();

        }
        /// <summary>
        /// /updating object feedback
        /// </summary>
        /// <param name="feedback">an object to be updated</param>
        public void UpdateFeedback(Feedback feedback)
        {
            _context.Update(feedback);
            _context.SaveChanges();
        }
    }
}
