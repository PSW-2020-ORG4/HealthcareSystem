using Backend.Model;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MyDbContext _context;

        public FeedbackRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Add(Feedback entity)
        {
            try
            {
                var feedback = entity.ToBackendFeedback();
                feedback.Id = 0;
                _context.Add(feedback);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new ValidationException(e.Message);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public void Update(Feedback entity)
        {
            try
            {
                var memento = entity.GetMemento();
                var feedback = _context.Feedbacks.Find(memento.Id);
                feedback.IsPublished = memento.IsPublished;
                _context.Update(feedback);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new ValidationException(e.Message);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public Feedback Get(int id)
        {
            try
            {
                var feedback = _context.Feedbacks.Find(id);
                if (feedback == null)
                    throw new NotFoundException("Feedback with id " + id + " does not exist.");
                return feedback.ToFeedback();
            }
            catch (FeedbackAndSurveyServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<Feedback> GetPublished()
        {
            try
            {
                return _context.Feedbacks.Where(f => f.IsPublished == true).Select(f => f.ToFeedback());
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<Feedback> GetUnpublished()
        {
            try
            {
                return _context.Feedbacks.Where(f => f.IsPublished == false).Select(f => f.ToFeedback());
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
