using Model.Patient;
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
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Window
    {
        
        public HomePageWindow()
        {
            InitializeComponent();
            Console.WriteLine();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void buttonNotification_Click(object sender, RoutedEventArgs e)
        {
            ReviewNotificationWindows reviewNotificationWindows = new ReviewNotificationWindows();
            reviewNotificationWindows.Show();
            this.Close();
        }

        private void buttonViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void buttonReviewAppointments_Click(object sender, RoutedEventArgs e)
        {
            ReviewExaminationsWindow reviewExaminationsWindow = new ReviewExaminationsWindow();
            reviewExaminationsWindow.Show();
            this.Close();
        }

        private void buttonCancelExamination_Click(object sender, RoutedEventArgs e)
        {
            CancelExaminationWindow cancelExaminationWindow = new CancelExaminationWindow();
            cancelExaminationWindow.Show();
            this.Close();
        }

        private void buttonReviewTherapy_Click(object sender, RoutedEventArgs e)
        {
            ReviewTherapyWindow reviewTherapyWindow = new ReviewTherapyWindow();
            reviewTherapyWindow.Show();
            this.Close();
        }

        private void buttonReviewSurvey_Click(object sender, RoutedEventArgs e)
        {
            SurveyWindow surveyWindow = new SurveyWindow();
            surveyWindow.Show();
            this.Close();
        }

        private void buttonMakeAppointments_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointmentWindow makeAppointmentWindow = new MakeAppointmentWindow();
            makeAppointmentWindow.Show();
            this.Close();
        }

        private void buttonRecommendAppoitments_Click(object sender, RoutedEventArgs e)
        {
            RecommendationAppointmentWindow recommendationAppointmentWindow = new RecommendationAppointmentWindow();
            recommendationAppointmentWindow.Show();
            this.Close();
        }

        private void buttonReviewNotifications_Click(object sender, RoutedEventArgs e)
        {
            ReviewNotificationWindows reviewNotificationWindows = new ReviewNotificationWindows();
            reviewNotificationWindows.Show();
            this.Close();
        }

        private void buttonReviewReport_Click(object sender, RoutedEventArgs e)
        {
            ReviewReportWindow reviewReportWindow = new ReviewReportWindow();
            reviewReportWindow.Show();
            this.Close();
        }

        private void buttonShowGraphicalEditor_Click(object sender, RoutedEventArgs e)
        {
            string currentUsername = MainWindow.patient.Username;

            GraphicalEditor.MainWindow graphicalEditorMainWindow = new GraphicalEditor.MainWindow("Patient", currentUsername);
            graphicalEditorMainWindow.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.O)
            {
                ReviewNotificationWindows reviewNotificationWindows = new ReviewNotificationWindows();
                reviewNotificationWindows.Show();
                this.Close();
            }
            else if (e.Key == Key.Escape)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.S)
            {
                MakeAppointmentWindow makeAppointmentWindow = new MakeAppointmentWindow();
                makeAppointmentWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.P)
            {
                RecommendationAppointmentWindow recommendationAppointmentWindow = new RecommendationAppointmentWindow();
                recommendationAppointmentWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.Z)
            {
                ReviewExaminationsWindow reviewExaminationsWindow = new ReviewExaminationsWindow();
                reviewExaminationsWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.T)
            {
                ReviewTherapyWindow reviewTherapyWindow = new ReviewTherapyWindow();
                reviewTherapyWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.C)
            {
                CancelExaminationWindow cancelExaminationWindow = new CancelExaminationWindow();
                cancelExaminationWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.I)
            {
                ReviewReportWindow reviewReportWindow = new ReviewReportWindow();
                reviewReportWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.A)
            {
                SurveyWindow surveyWindow = new SurveyWindow();
                surveyWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.E)
            {
                string currentUsername = MainWindow.patient.Username;

                GraphicalEditor.MainWindow graphicalEditorMainWindow = new GraphicalEditor.MainWindow("Patient", currentUsername);
                graphicalEditorMainWindow.ShowDialog();
            }
        }
    }
}
