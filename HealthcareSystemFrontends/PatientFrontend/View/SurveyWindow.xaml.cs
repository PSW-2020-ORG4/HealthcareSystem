using Controller.NotificationSurveyAndFeedback;
using Controller.UsersAndWorkingTime;
using Model.Patient;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for SurveyWindow.xaml
    /// </summary>
    public partial class SurveyWindow : Window
    {
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);

        SurveyController surveyController = new SurveyController();
        Patient patient;
        Survey survey = new Survey();
        Survey searchSurveyForPatient;
        public SurveyWindow()
        {
            InitializeComponent();
            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);

            searchSurveyForPatient = surveyController.ViewSurveyByJmbg(patient.Jmbg);

            if (searchSurveyForPatient != null)
            {
                survey = searchSurveyForPatient;

                if (survey.DoctorGrade == Grade.BAD)
                {
                    baddoc.IsChecked = true;
                }
                else if (survey.DoctorGrade == Grade.GOOD)
                {
                    gooddoc.IsChecked = true;
                }
                else if (survey.DoctorGrade == Grade.VERYGOOD)
                {
                    verygooddoc.IsChecked = true;
                }
                else
                {
                    excellentdoc.IsChecked = true;
                }

                if (survey.StaffGrade == Grade.BAD)
                {
                    badstaff.IsChecked = true;
                }
                else if (survey.StaffGrade == Grade.GOOD)
                {
                    goodstaff.IsChecked = true;
                }
                else if (survey.StaffGrade == Grade.VERYGOOD)
                {
                    verygoodstaff.IsChecked = true;
                }
                else
                {
                    excellentstaff.IsChecked = true;
                }

                if (survey.PrivacyGrade == Grade.BAD)
                {
                    badprivacy.IsChecked = true;
                }
                else if (survey.PrivacyGrade == Grade.GOOD)
                {
                    goodprivacy.IsChecked = true;
                }
                else if (survey.PrivacyGrade == Grade.VERYGOOD)
                {
                    verygoodprivacy.IsChecked = true;
                }
                else
                {
                    excellentprivacy.IsChecked = true;
                }

                comment.Text = survey.Content;

                button.Content = "Izmeni";
            }
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void buttonHomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void buttonViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            if (baddoc.IsChecked == true)
            {
                survey.DoctorGrade = Grade.BAD;
            }
            else if (gooddoc.IsChecked == true)
            {
                survey.DoctorGrade = Grade.GOOD;
            }
            else if (verygooddoc.IsChecked == true)
            {
                survey.DoctorGrade = Grade.VERYGOOD;
            }
            else
            {
                survey.DoctorGrade = Grade.EXCELLENT;
            }

            if (badstaff.IsChecked == true)
            {
                survey.StaffGrade = Grade.BAD;
            }
            else if (goodstaff.IsChecked == true)
            {
                survey.StaffGrade = Grade.GOOD;
            }
            else if (verygoodstaff.IsChecked == true)
            {
                survey.StaffGrade = Grade.VERYGOOD;
            }
            else
            {
                survey.StaffGrade = Grade.EXCELLENT;
            }

            if (badprivacy.IsChecked == true)
            {
                survey.PrivacyGrade = Grade.BAD;
            }
            else if (goodprivacy.IsChecked == true)
            {
                survey.PrivacyGrade = Grade.GOOD;
            }
            else if (verygoodprivacy.IsChecked == true)
            {
                survey.PrivacyGrade = Grade.VERYGOOD;
            }
            else
            {
                survey.PrivacyGrade = Grade.EXCELLENT;
            }

            survey.Content = comment.Text;

            if (searchSurveyForPatient != null)
            {
                surveyController.EditSurvey(survey);
                HomePageWindow homePageWindow = new HomePageWindow();
                homePageWindow.Show();
                this.Close();
                SuccessfulSendSurvey successfulSendSurvey = new SuccessfulSendSurvey();
                successfulSendSurvey.Show();
            }
            else
            {
                survey.JmbgOfPatient = MainWindow.patient.Jmbg;
                surveyController.AddSurvey(survey);
                HomePageWindow homePageWindow = new HomePageWindow();
                homePageWindow.Show();
                this.Close();
                SuccessfulSendSurvey successfulSendSurvey = new SuccessfulSendSurvey();
                successfulSendSurvey.Show();
                
            }

        }
    }
}
