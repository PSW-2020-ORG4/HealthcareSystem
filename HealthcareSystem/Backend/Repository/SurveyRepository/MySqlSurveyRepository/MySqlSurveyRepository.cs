using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MySqlSurveyRepository : ISurveyRepository
    {
        private readonly MyDbContext _context;

        public MySqlSurveyRepository(MyDbContext context)
        {
            _context = context;
        }

        public void AddSurvey(Survey survey)
        {
            _context.Surveys.Add(survey);
            _context.SaveChanges();
        }

        public Survey GetSurveyById(int id)
        {
            return _context.Surveys.Find(id);
        }

        public List<Survey> GetSurveysByJmbg(string jmbg)
        {
            return _context.Surveys.Where(f => f.DoctorJmbg.Equals(jmbg)).ToList();
        }

        public void UpdateSurvey(Survey survey)
        {
            _context.Update(survey);
            _context.SaveChanges();
        }
    }
}
