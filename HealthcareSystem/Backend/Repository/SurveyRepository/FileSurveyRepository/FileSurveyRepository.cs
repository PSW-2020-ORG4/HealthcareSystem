using Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class FileSurveyRepository : ISurveyRepository
    {
        private string _path;

        public FileSurveyRepository()
        {
            string fileName = "survey.json";
            _path = Path.GetFullPath(fileName);
        }

        private void WriteInFile(List<Survey> surveys)
        {
            string json = JsonConvert.SerializeObject(surveys);
            File.WriteAllText(_path, json);
        }
       
        private List<Survey> ReadFromFile()
        {
            List<Survey> surveys;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                surveys = JsonConvert.DeserializeObject<List<Survey>>(json);
            }
            else
            {
                surveys = new List<Survey>();
                WriteInFile(surveys);
            }
            return surveys;
        }

        public void AddSurvey(Survey survey)
        {
            throw new NotImplementedException();
        }

        public List<SurveyAboutDoctor> GetSurveysByDoctor(string jmbg)
        {
            throw new NotImplementedException();
        }

        public List<SurveyResult> GetSurveyResultsAboutMedicalStaff()
        {
            throw new NotImplementedException();
        }

        public List<SurveyResult> GetSurveyResultsAboutHospital()
        {
            throw new NotImplementedException();
        }

        public List<SurveyResult> GetSurveyResultsAboutDoctor(string jmbg)
        {
            throw new NotImplementedException();
        }
    }
}
