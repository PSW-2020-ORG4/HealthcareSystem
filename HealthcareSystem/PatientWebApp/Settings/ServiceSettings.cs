using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Settings
{
    public class ServiceSettings
    {
        public string PatientServiceUrl { get; set; }
        public string FeedbackAndSurveyServiceUrl { get; set; }
        public string UserServiceUrl { get; set; }
        public string NotificationServiceUrl { get; set; }
        public string ScheduleServiceUrl { get; set; }
    }
}
