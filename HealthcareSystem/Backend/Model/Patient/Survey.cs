/***********************************************************************
 * Module:  Survey.cs
 * Author:  Jelena Zeljko
 * Purpose: Definition of the Class Patient.Survey
 ***********************************************************************/

using Backend.Model.Enums;
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
            JmbgOfPatient = jmbgOfPatient;
            Content = content;
            DoctorGrade = doctorGrade;
            StaffGrade = staffGrade;
            PrivacyGrade = privacyGrade;
        }
        public Survey(Survey survey)
        {
            JmbgOfPatient = survey.JmbgOfPatient;
            Content = survey.Content;
            DoctorGrade = survey.DoctorGrade;
            StaffGrade = survey.StaffGrade;
            PrivacyGrade = survey.PrivacyGrade;
        }
    }
}
