/***********************************************************************
 * Module:  Feedback.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Users.Feedback
 ***********************************************************************/

using System;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Model.Users
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }
        public Patient Commentator { get; set; }

        public Feedback() { }
        public Feedback(int id,string comment,bool isPublished,Patient commentator)
        {
            Id = id;
            Comment = comment;
            IsPublished = isPublished;
            if(commentator != null) { Commentator = new Patient(commentator); }
            else { Commentator = new Patient();  }
        }

    }
}