/***********************************************************************
 * Module:  Feedback.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Users.Feedback
 ***********************************************************************/

using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Model.Users
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }

        [ForeignKey("Commentator")]
        public string CommentatorJmbg { get; set; }
        public virtual Patient Commentator { get; set; }

        public Feedback() { }
        public Feedback(int id,string comment,bool isPublished,Patient commentator)
        {
            Id = id;
            Comment = comment;
            IsPublished = isPublished;
            if(commentator != null) 
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