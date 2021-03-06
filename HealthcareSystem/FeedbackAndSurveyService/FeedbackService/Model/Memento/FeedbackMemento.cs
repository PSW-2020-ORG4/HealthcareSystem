﻿using System;

namespace FeedbackAndSurveyService.FeedbackService.Model.Memento
{
    public class FeedbackMemento : IMemento
    {
        public int Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Comment { get; set; }
        public Commentator Commentator { get; set; }
        public bool IsPublished { get; set; }
        public bool IsAllowedToPublish { get; set; }
    }
}
