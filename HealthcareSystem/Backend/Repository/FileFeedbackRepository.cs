using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.Repository
{
    class FileFeedbackRepository
    {
        private string _path;
        public FileFeedbackRepository()
        {
            string fileName = "feedback.json";
            _path = Path.GetFullPath(fileName);
        }
        public void AddFeedback(Feedback feedback)
        {
            List<Feedback> feedbacks = ReadFromFile();
            feedbacks.Add(feedback);
            WriteInFile(feedbacks);
        }

        public void PublishFeedback(int id)
        {
            List<Feedback> feedbacks = ReadFromFile();
            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.Id == id)
                {
                    feedback.IsPublished = true;
                    WriteInFile(feedbacks);
                    return;
                }
            }
        }

        public List<Feedback> GetPublishedFeedbacks()
        {
            List<Feedback> feedbacks = ReadFromFile();
            List<Feedback> publishedFeedbacks = new List<Feedback>();

            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.IsPublished)
                {
                    publishedFeedbacks.Add(feedback);
                }
            }
            return publishedFeedbacks;
        }

        public List<Feedback> GetUnpublishedFeedbacks()
        {
            List<Feedback> feedbacks = ReadFromFile();
            List<Feedback> unpublishedFeedbacks = new List<Feedback>();

            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.IsPublished)
                {
                    unpublishedFeedbacks.Add(feedback);
                }
            }
            return unpublishedFeedbacks;
        }
        private List<Feedback> ReadFromFile()
        {
            List<Feedback> feedbacks;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
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
            File.WriteAllText(_path, json);
        }
    }
}
