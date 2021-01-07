using Backend.Model;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public class CommentatorRepository : ICommentatorRepository
    {
        private readonly MyDbContext _context;

        public CommentatorRepository(MyDbContext context)
        {
            _context = context;
        }

        public Commentator Get(string id)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Commentator with jmbg " + id + " does not exist.");
                return new Commentator(patient.Jmbg, patient.Name, patient.Surname);
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
    }
}
