/***********************************************************************
 * Module:  Feedback.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Users.Feedback
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Feedback
   {
        public Stars Grade { get; set; }
        public string Comment { get; set; }

        public Feedback() { }
        public Feedback(Stars grade,string comment)
        {
            this.Grade = grade;
            this.Comment = comment;
        }

    }
}