/***********************************************************************
 * Module:  FeedbackRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.FeedbackRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class FeedbackRepository
   {
        private string path;

        public FeedbackRepository()
        {
            string fileName = "feedback.json";
            path = Path.GetFullPath(fileName);
        }
        public Model.Users.Feedback NewFeedback(Model.Users.Feedback feedback)
        {
            List<Feedback> feedbacks = ReadFromFile();
            feedbacks.Add(feedback);
            WriteInFile(feedbacks);
            return feedback;
        }

        public List<Feedback> GetAllFeedbacks()
        {
            List<Feedback> feedbacks = ReadFromFile();
            return feedbacks;
        }
        private List<Feedback> ReadFromFile()
        {
            List<Feedback> feedbacks;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(json);
            }
            else
            {
                feedbacks = new List<Feedback>();
                WriteInFile(feedbacks);
            }
            return feedbacks;
        }

        private void WriteInFile(List<Feedback> feedbacks)
        {
            string json = JsonConvert.SerializeObject(feedbacks);
            File.WriteAllText(path, json);
        }

    }
}