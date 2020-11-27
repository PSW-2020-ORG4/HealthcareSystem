/***********************************************************************
 * Module:  Feedback.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Users.Feedback
 ***********************************************************************/

using Castle.Components.DictionaryAdapter;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Model.NotificationSurveyAndFeedback
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime SendingDate { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }
        public bool IsAllowedToPublish { get; set; }

        [ForeignKey("Commentator")]
        public string CommentatorJmbg { get; set; }
        public virtual Patient Commentator { get; set; }

        public Feedback() { }
        public Feedback(int id, DateTime sendingDate, string comment, bool isPublished, bool isAllowedToPublish, Patient commentator)
        {
            Id = id;
            SendingDate = sendingDate;
            Comment = comment;
            IsPublished = isPublished;
            IsAllowedToPublish = isAllowedToPublish;
            if (commentator != null)
            {
                Commentator = new Patient(commentator);
                CommentatorJmbg = commentator.Jmbg;
            }
            else
            {
                Commentator = new Patient();
                CommentatorJmbg = null;
            }

        }

    }
}