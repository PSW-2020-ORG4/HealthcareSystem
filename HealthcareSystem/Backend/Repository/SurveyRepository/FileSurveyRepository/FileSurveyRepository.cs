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

        public void AddSurvey(Survey survey)
        {
            List<Survey> surveys = ReadFromFile();
            surveys.Add(survey);
            WriteInFile(surveys);
        }

        private void WriteInFile(List<Survey> surveys)
        {
            string json = JsonConvert.SerializeObject(surveys);
            File.WriteAllText(_path, json);
        }

        public Survey GetSurveyById(int id)
        {
            List<Survey> surveys = ReadFromFile();
            foreach (Survey survey in surveys)
            {
                if (survey.Id == id) { return survey; }
            }
            return null;
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

        public List<Survey> GetSurveysByJmbg(string jmbg)
        {
            List<Survey> surveys = ReadFromFile();
            List<Survey> surveysAboutDoctor = new List<Survey>();

            foreach (Survey survey in surveys)
            {
                if (survey.DoctorJmbg.Equals(jmbg))
                {
                    surveysAboutDoctor.Add(survey);
                }
            }
            return surveysAboutDoctor;
        }

        public void UpdateSurvey(Survey survey)
        {
            List<Survey> surveys = ReadFromFile();
            foreach (Survey s in surveys)
            {
                if (s.Id == survey.Id)
                {
                    WriteInFile(surveys);
                    return;
                }
            }
        }
    }
}
