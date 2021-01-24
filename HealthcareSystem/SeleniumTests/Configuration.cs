using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppE2ETests
{
    static class Configuration
    {
        public static string Host
        {
            get => Environment.GetEnvironmentVariable("PWA_HOST") ?? "http://localhost:65117";
        }

        public static string AddFeedbackPageURI
        {
            get => Host + "/html/add_feedback.html";
        }

        public static string BlockMaliciousPatientPageURI
        {
            get => Host + "/html/malicious_patients.html";
        }

        public static string LoginPageURI
        {
            get => Host + "/html/login.html";
        }

        public static string PatientHomePageURI
        {
            get => Host + "/html/patients_home_page.html";
        }

        public static string AdminHomePageURI
        {
            get => Host + "/html/admins_home_page.html";
        }

        public static string ExaminationPageURI
        {
            get => Host + "/html/patient_examinations.html";
        }

        public static string PublishFeedbackPageURI
        {
            get => Host + "/html/admins_home_page.html";
        }
    }
}
