/***********************************************************************
 * Module:  Survey.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Patient.Survey
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Patient
{
    public class Survey
    {
        public string JmbgOfPatient { get; set; }

        public string Content { get; set; }

        public Grade DoctorGrade { get; set; }

        public Grade StaffGrade { get; set; }

        public Grade PrivacyGrade { get; set; }

        public Survey()
        {
            
        }

        public Survey(string jmbgOfPatient, string content, Grade doctorGrade, Grade staffGrade, Grade privacyGrade)
        {
            this.JmbgOfPatient = jmbgOfPatient;
            this.Content = content;
            this.DoctorGrade = doctorGrade;
            this.StaffGrade = staffGrade;
            this.PrivacyGrade = privacyGrade;
        }
        public Survey(Survey survey)
        {
            this.JmbgOfPatient = survey.JmbgOfPatient;
            this.Content = survey.Content;
            this.DoctorGrade = survey.DoctorGrade;
            this.StaffGrade = survey.StaffGrade;
            this.PrivacyGrade = survey.PrivacyGrade;
        }
    }
}
